using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;

namespace Showdown.Protocol.Tests
{
    [Category("IntegrationTest")]
    public class ShowdownProtocolClientTest
    {
        [Test]
        public async Task TestBasicConnection()
        {
            CancellationToken token = CancellationToken.None;
            
            ShowdownProtocolClient client = new(NullLogger<ShowdownProtocolClient>.Instance);
            WebsocketMessageStream stream = new();
            await stream.Connect(new Uri("ws://localhost:8000/showdown/websocket"), token);
            Task clientRunTask = client.Run(stream, token);

            client.Any += (sender, args) =>
            {
                Console.WriteLine($"type '{args.Type}' in room '{args.RoomId}': {args.Data}");
            };
            await client.SendAsync(null, "/cmd rooms", token);
            await Task.Delay(TimeSpan.FromSeconds(1), token);
            
            await stream.Disconnect(token);
            await clientRunTask;
        }
    }
}
