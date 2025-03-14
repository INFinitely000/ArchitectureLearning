using System;

namespace Console
{
    [AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = false)]
    public class CommandAttribute : Attribute
    {
        public string Command { get; private set; }

        public CommandAttribute() {}

        public CommandAttribute(string command) =>
            Command = command;
    }
}