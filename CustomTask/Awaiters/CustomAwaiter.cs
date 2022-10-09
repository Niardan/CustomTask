using System.Runtime.CompilerServices;

namespace CustomTask.Awaiters
{
    [AsyncMethodBuilder(typeof(SimpleTaskMethodBuilder<>))]
    public class CustomAwaiter<T> : IAwaiter<T>
    {
        private Action _continuation;
        private T _value;

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

        public bool IsCompleted { get; private set; }

        public T GetResult()
        {
            return _value;
        }
    }
}

