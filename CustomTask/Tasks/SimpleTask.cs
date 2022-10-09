using CustomTask.Awaiters;

namespace CustomTask.Tasks
{
    public class SimpleTask<T>
    {
        protected CancellationTokenSource _cancel = new CancellationTokenSource();
        public SimpleAwaiter<T> _task;

        public SimpleTask()
        {
            _task = new SimpleAwaiter<T>();
        }

        public SimpleAwaiter<T> Wait(int timeout = 0)
        {
            if (timeout > 0)
            {
                Timeout(timeout);
            }
            return _task;
        }

        public void Complete(T value)
        {
            if (_task != null)
            {
                var task = _task;
                _task = null;
                task.Complete(value);
                _cancel?.Cancel();
            }
        }
        
        protected virtual async void Timeout(int timeout)
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
            _task?.SetException(new TimeoutException());
        }
    }
}

