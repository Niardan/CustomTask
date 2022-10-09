namespace CustomTask.Tasks
{
    public class ResultSimpleTask : SimpleTask<TaskResult>
    {
        public void Complete(bool result, string log = "")
        {
            Complete(new TaskResult(result, log));
        }

        protected override async void Timeout(int timeout)
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
           Complete(false, "timeout");
        }
    }
}

