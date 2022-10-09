using System.Runtime.CompilerServices;

namespace CustomTask.Awaiters
{
    public interface IAwaiter<T> : INotifyCompletion
    {
        bool IsCompleted { get; }
        T GetResult();
        void Complete(T value);
        IAwaiter<T> GetAwaiter();
        void SetException(Exception ex);
    }
}