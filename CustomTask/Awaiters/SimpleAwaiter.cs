using System.Runtime.CompilerServices;

namespace CustomTask.Awaiters
{
    [AsyncMethodBuilder(typeof(SimpleTaskMethodBuilder<>))]
    public class SimpleAwaiter<T> : IAwaiter<T>
    {
        private Action _continuation;
        private T _value;
        private Exception _ex;

        public void OnCompleted(Action continuation)
        {
            if (IsCompleted)
            {
                continuation?.Invoke();
            }
            else
            {
                _continuation = continuation;
            }
        }

        public void Complete(T value)
        {
            _value = value;
            IsCompleted = true;
            _continuation?.Invoke();
        }

        public IAwaiter<T> GetAwaiter() => this;
        public void SetException(Exception ex)
        {
            _ex = ex;
            IsCompleted = true;
            _continuation?.Invoke();
        }

        public bool IsCompleted { get; private set; }

        public T GetResult()
        {
            if (_ex != null)
                throw _ex;
            return _value;
        }
    }
}

