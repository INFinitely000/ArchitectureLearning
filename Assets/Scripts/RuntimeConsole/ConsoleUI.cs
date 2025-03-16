using System;
using System.Linq;
using UnityEngine;

namespace RuntimeConsole
{
    public class ConsoleUI : MonoBehaviour
    {
        private readonly string _inputFieldName = "consoleInputField";
        private string _consoleInputText = "";

        private GUIContent _content;
        private GUIStyle _style;


        private void OnGUI()
        {
            GUI.backgroundColor = new Color(0f, 0f, 0f, 0.5f);

            if (IsReturnKeyPressed())
                Process();

            if (IsEscapeKeyPressed())
                CloseInputField();

            if (IsControlKeyPressed())
                OpenInputField();

            var messageRect = new Rect();
            messageRect.x = 10;
            messageRect.y = Screen.height - 25;

            for (int index = Console.Messages.Count - 1; index > 0; index--)
            {
                _content.text = Console.Messages.ToArray()[index];

                _style.CalcMinMaxWidth(_content, out float minWidth, out float maxWidth);

                messageRect.width = minWidth + 10;
                messageRect.height = (int)_style.CalcHeight(_content, minWidth);

                messageRect.y -= messageRect.height;    
            
                GUI.Label(messageRect, _content, _style);
            }
        }

        private void OpenInputField()
        {
            var rect = new Rect(x: 10, y: Screen.height - 25, width: Screen.width - 20, height: 25);
            
            GUI.SetNextControlName(_inputFieldName);
            _consoleInputText = GUI.TextField(rect, _consoleInputText);

            GUI.FocusControl(_inputFieldName);
        }

        private void CloseInputField()
        {
            GUI.FocusControl("");
            _consoleInputText = "";
        }

        private void Process()
        {
            GUI.FocusControl("");
            Console.Process(_consoleInputText);
            _consoleInputText = "";
        }

        private bool IsControlKeyPressed() => 
            (GUI.GetNameOfFocusedControl() != _inputFieldName && 
             Event.current.type == EventType.KeyDown && 
             Event.current.keyCode == KeyCode.RightControl) || 
            GUI.GetNameOfFocusedControl() == _inputFieldName;

        private bool IsEscapeKeyPressed() => 
            GUI.GetNameOfFocusedControl() == _inputFieldName && 
            Event.current.type == EventType.KeyDown && 
            Event.current.keyCode == KeyCode.Escape;

        private bool IsReturnKeyPressed() =>
            GUI.GetNameOfFocusedControl() == _inputFieldName && 
            Event.current.type == EventType.KeyDown && 
            Event.current.keyCode == KeyCode.Return;

        #region Instance

        private static ConsoleUI _instance;
        private static bool _isAllowToCreateInstance;
        
        [RuntimeInitializeOnLoadMethod]
        private static void CreateInstance()
        {
            _isAllowToCreateInstance = true;

            _instance = new GameObject()
                .AddComponent<ConsoleUI>();

            DontDestroyOnLoad(_instance);
            _instance.CreateConsoleStyle();
            
            _isAllowToCreateInstance = false;
        }

        private void CreateConsoleStyle()
        {
            _style = new GUIStyle();
            _content = new GUIContent();
            
            _style.fontSize = 12;
            _style.normal.textColor = Color.white;
            _style.contentOffset = Vector2.right * 5;
            
            _style.normal.background = new Texture2D(1,1);
        }

        private void Awake()
        {
            if (_isAllowToCreateInstance == false)
                DestroyImmediate(this);
        }

        #endregion
    }
}