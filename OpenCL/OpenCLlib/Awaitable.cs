using System;
using System.Threading;
using System.Threading.Tasks;

namespace OpenCL
{
    internal class AsyncManualResetEvent
    {
        private readonly ManualResetEvent evt;
        private TimeSpan timeout;
        public AsyncManualResetEvent(bool Condition) : this(Condition, Timeout.InfiniteTimeSpan)
        {

        }

        public AsyncManualResetEvent(bool Condition, TimeSpan wait)
        {
            evt = new ManualResetEvent(Condition);
            timeout = wait;
        }

        public Task WaitAsync()
        {
            TaskCompletionSource<object> tcs = new TaskCompletionSource<object>();
            RegisteredWaitHandle registration = ThreadPool.RegisterWaitForSingleObject(evt, (state, timedOut) =>
            {
                TaskCompletionSource<object> localTcs = (TaskCompletionSource<object>)state;
                if (timedOut)
                {
                    localTcs.TrySetCanceled();
                }
                else
                {
                    localTcs.TrySetResult(null);
                }
            }, tcs, timeout, executeOnlyOnce: true);
            tcs.Task.ContinueWith((_, state) => ((RegisteredWaitHandle)state).Unregister(null), registration, TaskScheduler.Default);
            return tcs.Task;
        }

        public void WaitSync()
        {
            evt.WaitOne();
        }

        internal void Set()
        {
            evt.Set();
        }
    }

}
