using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Service;
using Unity.VisualScripting;
using UnityEngine;

namespace Console
{
    public static class Console
    {
        public static IReadOnlyDictionary<string, List<CommandData>> Commands => _commands;
        public static IReadOnlyCollection<string> Messages => _messages;

        private static Dictionary<string, List<CommandData>> _commands =
            new Dictionary<string, List<CommandData>>();
        private static Queue<string> _messages = new Queue<string>(capacity: _messagesMaxCount);
        private const int _messagesMaxCount = 25;
    
        private static readonly Regex _commandRegex = new Regex(@"^[a-zA-Z_]+$");


        public static void Register(string command, CommandData data)
        {
            if (IsCommandValid(command) == false)
                throw new ArgumentException(command);

            if (_commands.TryGetValue(command, out var datas))
            {
                datas.Add(data);
            }
            else
            {
                _commands.Add(command, new List<CommandData>() { data });
            }
        }

        public static void Process(string expression)
        {
            if (IsCommand(expression))
                ProcessCommand(expression);
            else
                ProcessMessage(expression);
        }

        private static void ProcessCommand(string expression)
        {
            var parts = expression.Remove(0, 1).Split(' ');
            var command = parts[0];
            var parameterValues = parts.Length > 1 ? parts[1..parts.Length] : Array.Empty<string>();

            TryIdentifyParameters(parameterValues, out var parameterTypes, out var parameters);
            
            if (_commands.TryGetValue(command, out var datas))
            {
                foreach (var data in datas)
                {
                    if (data.parameters.SequenceEqual(parameterTypes))
                    {
                        data.method.Invoke(null, parameters);

                        return;
                    }
                }
                
                ProcessMessage("There is no override of a command with such parameters: " + parameterTypes.Length);
            }
            else
            {
                ProcessMessage("Command is not exist");
            }
        }

        private static bool TryIdentifyParameters(string[] parts, out Type[] parameterTypes, out object[] parameters)
        {
            parameterTypes = new Type[parts.Length];
            parameters = new object[parts.Length];

            if (parts.Length > 0)
            {
                for (int index = 0; index < parts.Length; index++)
                {
                    if (parts[index].TryIdentifyType(out var type, out var value))
                    {
                        parameterTypes[index] = type;
                        parameters[index] = value;
                    }
                    else
                    {
                        ProcessMessage("Unknown parameter: " + parts[index]);

                        return false;
                    }
                }
            }

            return true;
        }

        private static void ProcessMessage(string expression)
        {
            if (IsExpressionValid(expression) == false) return;
            
            if (_messages.Count >= _messagesMaxCount)
                _messages.Dequeue();
            
            _messages.Enqueue(expression);
        }
    
    
        private static bool IsCommand(string command) => command.StartsWith('/');
    
        private static bool IsCommandValid(string command)
        {
            if (_commands.ContainsKey(command)) return false;

            return _commandRegex.IsMatch(command);
        }
        
        private static bool IsExpressionValid(string expression) =>
            expression != string.Empty;


        static Console()
        {
            Application.logMessageReceived += OnLogMessageRecieved;

            RegisterCommands();
        }

        private static void RegisterCommands()
        {
            string[] ignoredAssemblies = new string[]
            {
                "Unity",
                "System",
                "Mono.",
                "mscorlib",
                "netstandard",
                "TextMeshPro",
                "Microsoft.GeneratedCode"
            };
            
            var methods = AppDomain.CurrentDomain.GetAssemblies()
                .Where(a => a.IsDynamic == false && ignoredAssemblies.Contains(a.GetName().Name) == false)
                .SelectMany(x => x.GetExportedTypes())
                .SelectMany(x => x.GetMethods())
                .Where(z => z.IsStatic);

            foreach (var method in methods)
            {
                var attribute = method.GetCustomAttribute<CommandAttribute>();

                if (attribute != null)
                {
                    var parameters = method.GetParameters()
                        .Select(p => p.ParameterType)
                        .ToArray();

                    if (parameters.Any(p => p.IsClass || p.IsInterface))
                    {
                        Debug.LogError("Method have a non-struct parameters: " + method);
                    
                        continue;
                    }

                    var data = new CommandData(method, parameters);
                    
                    Register(attribute.Command, data);
                    
                    Debug.LogFormat("Registered command with: Name: {0}, Parameters: {1}", attribute.Command, parameters.Length);
                }
            }
        }

        private static void OnLogMessageRecieved(string condition, string stacktrace, LogType type) =>
            ProcessMessage(condition);

        [Command("penisSize")]
        public static void TestMessage(int size)
        {
            ProcessMessage("Mr.INF's penis size is: " + size.ToString());
        }
    }
}