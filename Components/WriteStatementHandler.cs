namespace EasyAutoScript.Components
{
    public class WriteStatementHandler(object value)
    {
        public void Execute()
        {
            if (value is Array array)
            {
                foreach (var item in array)
                {
                    Console.WriteLine(item);
                }
                return;
            }
            Console.WriteLine(value);
        }
    }
}