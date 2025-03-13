using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Unity.VisualScripting;
using UnityEngine;

namespace Console
{
    public static class Console
    {
        public static IReadOnlyDictionary<string, Action<ConsoleEventData>> Commands => _commands;
        public static IReadOnlyCollection<string> Messages => _messages;

        private static Dictionary<string, Action<ConsoleEventData>> _commands =
            new Dictionary<string, Action<ConsoleEventData>>();
        private static Queue<string> _messages = new Queue<string>(capacity: _messagesMaxCount);
        private const int _messagesMaxCount = 25;
    
        private static readonly Regex _commandRegex = new Regex("^[abc]");


        public static void Register(string command, Action<ConsoleEventData> callback)
        {
            if (IsCommandValid(command) == false)
                throw new ArgumentException(command);

            _commands.Add(command, callback);
        }

        public static void Unregister(string command) => 
            _commands.Remove(command);

        public static void Process(string expression)
        {
            if (IsCommand(expression))
                ProcessCommand(expression);
            else
                ProcessMessage(expression);
        }

        private static void ProcessCommand(string expression)
        {
            var parts = expression.Split(' ');
            var command = parts[0];
            var args = parts.Length > 1 ? parts[1..parts.Length] : null;
            
            if (_commands.TryGetValue(command, out var callback))
            {
                var eventData = new ConsoleEventData(args);
                callback.Invoke(eventData);
            }
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
        }

        private static void OnLogMessageRecieved(string condition, string stacktrace, LogType type) =>
            ProcessMessage(condition);
    }
}