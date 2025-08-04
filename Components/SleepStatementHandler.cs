namespace EasyAutoScript.Components
{
    public class SleepStatementHandler(double value)
    {
        public async Task Execute()
        {
            await Task.Delay(Convert.ToInt32(value));
        }
    }
}