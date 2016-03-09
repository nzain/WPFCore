using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace WPFCore.Commands
{
    /// <summary>An asynchronous command to be implemented in a separated class.</summary>
    public abstract class AsyncCommandBase : CommandBase, ICancellableCommand
    {
        private bool _isBusy;

        /// <summary>Gets the cancellation token source or <c>null</c> if the command is not running.</summary>
        protected CancellationTokenSource CancellationTokenSource { get; private set; }

        /// <summary>Gets a flag indicating this command is currently running.</summary>
        public bool IsBusy
        {
            get { return this._isBusy; }
            private set
            {
                this._isBusy = value;
                this.OnPropertyChanged();
                this.RaiseCanExecuteChanged();
            }
        }

        /// <summary>Gets a flag indicating that cancellation is requested on this command.</summary>
        public bool IsCancellationRequested
        {
            get
            {
                var cts = this.CancellationTokenSource;
                return cts != null && cts.IsCancellationRequested;
            }
        }

        /// <summary>Executes the asynchronous operation.</summary>
        /// <param name="parameter">An optional parameter to be bound from XAML.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>An awaitable task.</returns>
        protected abstract Task ExecuteAsync(object parameter, CancellationToken cancellationToken);

        /// <summary>Checks if this command can execute right now.</summary>
        /// <param name="parameter">An optional parameter to be bound from XAML.</param>
        /// <returns><c>true</c> if this command can execute.</returns>
        public new abstract bool CanExecute(object parameter);

        /// <summary>Listen on the given viewmodel and its property. This call can be chained to listen on multiple properties.</summary>
        /// <typeparam name="TViewModel">The type of viewmodel to listen on.</typeparam>
        /// <typeparam name="TPropertyType">The type of property to listen on.</typeparam>
        /// <param name="viewModel">The viewmodel.</param>
        /// <param name="propertyExpression">An expresion like <c>vm => vm.SomeProperty</c> that identifies the property this
        /// command's CanExecute status depends on.</param>
        /// <returns>This command for chaining multiple calls.</returns>
        public new AsyncCommandBase ListenOn<TViewModel, TPropertyType>(TViewModel viewModel,
            Expression<Func<TViewModel, TPropertyType>> propertyExpression)
            where TViewModel : INotifyPropertyChanged
        {
            return (AsyncCommandBase)base.ListenOn(viewModel, propertyExpression);
        }

        /// <summary>Listen on the given viewmodel's validation errors.</summary>
        /// <param name="viewModel">The validatable viewmodel.</param>
        /// <returns>This command for chainging multiple calls.</returns>
        public new AsyncCommandBase ListenOnValidationErrors(INotifyDataErrorInfo viewModel)
        {
            return (AsyncCommandBase)base.ListenOnValidationErrors(viewModel);
        }

        #region ICommand Members

        [DebuggerStepThrough]
        public override async void Execute(object parameter)
        {
            // async void is REALLY evil, unless this is an UI event (e.g. click). 
            // Noone will (and noone can!) wait for this to finish, it will run like a fire & forget thread.
            // Never do this at home. Instead, always use async Task or async Task<T>!
            // Confused? Ask Patrick or google, why async void is bad :)
            using (this.CancellationTokenSource = new CancellationTokenSource())
            {
                this.IsBusy = true;
                try
                {
                    await this.ExecuteAsync(parameter, this.CancellationTokenSource.Token);
                }
                finally
                {
                    this.IsBusy = false;
                    this.CancellationTokenSource = null;
                }
            }
        }

        #endregion

        #region ICancellableCommand Members

        /// <summary>Requests cancellation and returns immediately. Implementations should monitor the underlying
        /// <c>CancellationToken</c> and react accordingly.</summary>
        public virtual void Cancel()
        {
            var cts = this.CancellationTokenSource;
            if (cts != null && !cts.IsCancellationRequested)
            {
                cts.Cancel();
            }
        }

        /// <summary>Requests cancellation and waits asynchronously for completion.</summary>
        /// <param name="millisecondsTimeout">[Optional] The timeout in milliseconds (defaults to 2000).</param>
        /// <returns>An awaitable task.</returns>
        public async Task CancelAndWaitAsync(int millisecondsTimeout = 2000)
        {
            var cts = this.CancellationTokenSource;
            if (cts != null && !cts.IsCancellationRequested)
            {
                cts.Cancel();
            }

            const int delayMilliseconds = 100;
            int count = 0;
            while (this.IsBusy)
            {
                // ReSharper disable once MethodSupportsCancellation - The cancellation token is already cancelled...
                await Task.Delay(delayMilliseconds);
                count++;
                int milliseconds = count * delayMilliseconds;
                if (milliseconds > millisecondsTimeout) // probably the underlying thread does not check its token!
                {
                    string msg = $"Cancellation timed out after {millisecondsTimeout}ms";
                    throw new TimeoutException(msg);
                }
            }
        }

        #endregion
    }
}