using CustomTask.Awaiters;

namespace CustomTask.Tasks
{
    public class TimeoutResultSimpleTask : ResultSimpleTask
    {
        protected CancellationTokenSource _cancel = new CancellationTokenSource();

        public async CustomAwaiter<TaskResult> Wait(int timeout)
        {
            Timeout(timeout);
            var result = await Wait();
            _cancel?.Cancel();
            return result;
        }

        private async void Timeout(int timeout)
        {
            try
            {
                await Task.Delay(timeout, _cancel.Token);
            }
            catch
            {
                return;
            }

            _cancel = null;
            Complete(new TaskResult(false, "timeout"));
        }
    }
}