using CommonClassLibrary;

namespace JobTest1
{
    public class Class1 : INikhJob
    {
        public bool Run()
        {
            string filePath = "E:\\DotNet\\Scheduler\\DataFiles\\JobTest1.txt";
            string message = $"Called me at {DateTime.Now}";

            // Append the message to the file
            File.AppendAllText(filePath, message + Environment.NewLine);

            return true;
        }
    }
}
