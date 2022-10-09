namespace CustomTask.Tasks
{
    public class ResultSimpleTask : SimpleTask<TaskResult>
    {
        public void Complete(bool result, string log = "")
        {
            Complete(new TaskResult(result, log));
        }
    }
}

