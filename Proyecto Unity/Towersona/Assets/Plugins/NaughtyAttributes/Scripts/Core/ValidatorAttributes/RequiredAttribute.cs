using System;

namespace NaughtyAttributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = true)]
    public class RequiredAttribute : ValidatorAttribute
    {
        public string Message { get; private set; }
        public bool LogToConsole { get; private set; }

        public RequiredAttribute(string message = null, bool logToConsole = false)
        {
            this.Message = message;
            this.LogToConsole = logToConsole;
        }
    }
}
