namespace CustomTask.Tasks
{
    public class TaskResult
    {
        public TaskResult(bool isSuccess, string log)
        {
            IsSuccess = isSuccess;
            Log = log;
        }

        public TaskResult(bool isSuccess)
        {
            IsSuccess = isSuccess;
            Log = "";
        }

        public bool IsSuccess { get; }
        public string Log { get; }
    }
}