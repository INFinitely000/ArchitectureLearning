using System.Reflection;
using System;

namespace Console
{
    public struct CommandData
    {
        public MethodInfo method;
        public Type[] parameters;
        
        public CommandData(MethodInfo method, Type[] parameters)
        {
            this.method = method;
            this.parameters = parameters;
        }
    }
}