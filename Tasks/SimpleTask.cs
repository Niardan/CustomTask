using CustomTask.Awaiters;

namespace CustomTask.Tasks;

public class SimpleTask<T>
{
    public CustomAwaiter<T> _task;

    public SimpleTask()
    {
        _task = new CustomAwaiter<T>();
    }

    public CustomAwaiter<T> Wait()
    {
        return _task;
    }

    public void Complete(T value)
    {
        if (_task != null)
        {
            var task = _task;
            _task = null;
            task.Complete(value);
        }
    }
}