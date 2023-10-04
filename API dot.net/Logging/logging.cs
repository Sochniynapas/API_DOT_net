namespace API_dot.net.Logging
{
    public class logging : ILogging
    {
        public void Log(string message, string type)
        {
            if(type == "error")
            {
                Console.WriteLine("ERROR: " + message);

            }
            else
            {
                Console.WriteLine(message);

            }
        }
    }
}
