namespace SW.TechnicalAssignment
{
    using System;
    using Interfaces;

    public class ConsoleLogger : ILogger
    {
        public void Log(object value)
        {
            Console.WriteLine(value);
        }
    }
}