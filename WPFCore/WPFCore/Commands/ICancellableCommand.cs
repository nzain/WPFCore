using System.Threading.Tasks;
using System.Windows.Input;

namespace WPFCore.Commands
{
    /// <summary>Defines a cancellable (long running) command.</summary>
    public interface ICancellableCommand : ICommand
    {
        /// <summary>Requests cancellation and returns immediately. Implementations should monitor the underlying
        /// <c>CancellationToken</c> and react accordingly.</summary>
        void Cancel();

        /// <summary>Requests cancellation and waits asynchronously for completion.</summary>
        /// <param name="millisecondsTimeout">[Optional] The timeout in milliseconds (defaults to 2000).</param>
        /// <returns>An awaitable task.</returns>
        Task CancelAndWaitAsync(int millisecondsTimeout = 2000);
    }
}