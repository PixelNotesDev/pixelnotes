namespace WebPGenerator.Utilities
{
    public class ProgressReporter
    {
        private bool _keepReporting = true;

        public async Task ShowProgressAsync()
        {
            string progressMessage = "Work in progress";
            int dotCount = 0;

            while (_keepReporting)
            {
                Console.Write("\r" + progressMessage + new string('.', dotCount));
                await Task.Delay(1000);
                dotCount = (dotCount + 1) % 10;
            }
        }

        public void Stop()
        {
            _keepReporting = false;
        }
    }
}
