using System.Runtime.CompilerServices;
using CustomTask.Awaiters;

namespace CustomTask
{
    public sealed class SimpleTaskMethodBuilder<T>
    {
        private readonly CustomAwaiter<T> _awaiter = new CustomAwaiter<T>();

        public static SimpleTaskMethodBuilder<T> Create() => new SimpleTaskMethodBuilder<T>();

        public void SetResult(T result)
        {
            _awaiter.Complete(result);
        }

        public void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }

        public void AwaitUnsafeOnCompleted<TAwaiter, TStateMachine>
            (ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : ICriticalNotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        public void AwaitOnCompleted<TAwaiter, TStateMachine>
            (ref TAwaiter awaiter, ref TStateMachine stateMachine)
            where TAwaiter : INotifyCompletion where TStateMachine : IAsyncStateMachine
        {
            awaiter.OnCompleted(stateMachine.MoveNext);
        }

        public void Start<TStateMachine>(ref TStateMachine stateMachine)
            where TStateMachine : IAsyncStateMachine
        {
            stateMachine.MoveNext();
        }

        public void SetException(Exception exception)
        {
            throw exception;
        }

        public CustomAwaiter<T> Task => _awaiter;
    }
}