namespace EasyAutoScript.Components
{
    public class ClearStatementHandler()
    {
        public static void Execute()
        {
            try
            {
                Console.Clear();
            }
            catch (IOException ex)
            {
                Console.WriteLine($"Console clear operation skipped: {ex.Message}");
            }
        }
    }
}