using System.Threading;
using System.Threading.Tasks;

namespace Showdown.Protocol
{
    public interface IAsyncMessageStream
    {
        Task<string?> ReadAsync(CancellationToken? cancellationToken = null);
        Task WriteAsync(string message, CancellationToken? cancellationToken = null);
    }
}
