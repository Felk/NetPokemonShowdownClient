using System;
using System.IO;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Showdown.Protocol
{
    // TODO replace with properly tested and robust websocket connection
    public sealed class WebsocketMessageStream : IAsyncMessageStream, IAsyncDisposable
    {
        private static readonly Encoding Utf8NoBom = new UTF8Encoding(false);
        private ClientWebSocket _ws = null!;

        private readonly byte[] _readBuffer = new byte[8192];

        public async Task Connect(
            Uri websocketUri,
            CancellationToken cancellationToken)
        {
            _ws = new ClientWebSocket();
            await _ws.ConnectAsync(websocketUri, cancellationToken);
        }

        public async Task Disconnect(CancellationToken cancellationToken)
        {
            await _ws.CloseOutputAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
            await _ws.CloseAsync(WebSocketCloseStatus.NormalClosure, string.Empty, cancellationToken);
        }

        public async ValueTask DisposeAsync()
        {
            if (_ws.State == WebSocketState.Open) await Disconnect(CancellationToken.None);
            _ws.Dispose();
        }

        public async Task<string?> ReadAsync(CancellationToken? cancellationToken = null)
        {
            var bufferSegment = new ArraySegment<byte>(_readBuffer);
            await using var ms = new MemoryStream();
            while (true)
            {
                WebSocketReceiveResult result = await _ws.ReceiveAsync(bufferSegment,
                    cancellationToken ?? CancellationToken.None);
                if (result.CloseStatus != null) return null;
                //todo close message
                if (result.MessageType != WebSocketMessageType.Text) throw new NotSupportedException();
                await ms.WriteAsync(_readBuffer, 0, result.Count,
                    cancellationToken ?? CancellationToken.None);
                if (result.EndOfMessage) break;
            }

            ms.Seek(0, SeekOrigin.Begin);
            return await new StreamReader(ms, Utf8NoBom).ReadToEndAsync();
        }

        public async Task WriteAsync(string message, CancellationToken? cancellationToken = null)
        {
            await _ws.SendAsync(Utf8NoBom.GetBytes(message), WebSocketMessageType.Text, true,
                cancellationToken ?? CancellationToken.None);
        }
    }
}
