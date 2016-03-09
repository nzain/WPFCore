using System;

namespace WPFCore.Commands
{
    /// <summary>An asynchronous command with progress notification to be implemented in a separated class.</summary>
    public abstract class AsyncProgressCommandBase : AsyncCommandBase
    {
        private readonly IProgress<int> _progressCallback;
        private int _lastProgress;

        protected AsyncProgressCommandBase(IProgress<int> progressCallback)
        {
            this._progressCallback = progressCallback;
        }

        protected void UpdateProgress(int current, int max)
        {
            int progress = (int)(100.0 * current / max);
            this.UpdateProgressInternal(progress);
        }

        protected void UpdateProgress(long current, int max)
        {
            int progress = (int)(100.0 * current / max);
            this.UpdateProgressInternal(progress);
        }

        protected void UpdateProgress(int current, long max)
        {
            int progress = (int)(100.0 * current / max);
            this.UpdateProgressInternal(progress);
        }

        protected void UpdateProgress(long current, long max)
        {
            int progress = (int)(100.0 * current / max);
            this.UpdateProgressInternal(progress);
        }

        private void UpdateProgressInternal(int progress)
        {
            if (progress != this._lastProgress)
            {
                this._lastProgress = progress;
                this._progressCallback.Report(progress);
            }
        }

    }
}