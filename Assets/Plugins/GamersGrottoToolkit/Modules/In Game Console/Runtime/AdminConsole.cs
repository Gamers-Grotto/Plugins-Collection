using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using GamersGrotto.Core;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GamersGrotto.In_Game_Console
{
    public class AdminConsole : Singleton<AdminConsole>
    {
        [Header("Settings")]
        [SerializeField] private bool timeStampHistory = true;
        [SerializeField] private bool clearConsoleOnStart = true;
        [SerializeField] private bool showAllCommandsOnStart = true;
        [SerializeField] private bool showUnityConsoleLogs = true;
        [SerializeField] private float fadeInOutDuration = 0.5f;
        
        [Header("Controls")]
        [SerializeField] private InputAction consoleKey;
        [SerializeField] private InputAction autoCompleteKey;

        [Header("Dependencies")]
        [Tooltip("This GameObject will be enabled and disabled to show/hide the console")]
        [SerializeField] private GameObject parentGameObject;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private TMP_Text history;
        [SerializeField] private CanvasGroup canvasGroup;

        private CommandManager commandManager;
        private bool isOpen;
        private float scrollRectPos;
        private Coroutine fadeEffectRoutine;
        private List<string> probableCommands = new();

        public event Action<bool> ConsoleToggledEvent; 
        
        public List<string> CommandList 
        {
            get
            {
                var commands = commandManager.CommandList;
                commands.Sort();
                return commands;
            }
        }

        public bool IsOpen => isOpen;

        #region MonoBehaviour

        private void OnEnable()
        {
            consoleKey.Enable();
            consoleKey.performed += OnConsoleKeyPressed;

            autoCompleteKey.Enable();
            autoCompleteKey.performed += OnAutoCompletePressed;
        
            inputField.onSubmit.AddListener(OnInputFieldSubmit);
            inputField.onValueChanged.AddListener(OnInputFieldValueChanged);
            inputField.onSelect.AddListener(OnInputFieldSelected);
            
            Application.logMessageReceivedThreaded += OnDebugLogReceived;
        }

        private async void Start()
        {
            commandManager = new CommandManager();
            commandManager.RegisterCommands();
            
            if(clearConsoleOnStart)
                Clear();
            
            if(showAllCommandsOnStart)
                await commandManager.RunCommand("commands", Array.Empty<string>());
            
            canvasGroup.alpha = 0f;
            parentGameObject.SetActive(false);
        }

        private void OnDisable()
        {
            consoleKey.performed -= OnConsoleKeyPressed;
            consoleKey.Disable();
            
            autoCompleteKey.Disable();
            autoCompleteKey.performed -= OnAutoCompletePressed;
        
            inputField.onSubmit.RemoveListener(OnInputFieldSubmit);
            inputField.onValueChanged.RemoveListener(OnInputFieldValueChanged);
            inputField.onSelect.RemoveListener(OnInputFieldSelected);
            
            Application.logMessageReceivedThreaded -= OnDebugLogReceived;
        }

        #endregion

        #region Public Methods
        
        public void Add(string message)
        {
            var stamp = timeStampHistory 
                ? GetTimeStamp() 
                : string.Empty;

            var append = $"\n{stamp}{message}";
            history.text += append;
        }

        public void Clear()
        {
            history.text = string.Empty;
        }

        #endregion

        #region Private Methods

        private void Show()
        {
            inputField.text = string.Empty;
            parentGameObject.SetActive(true);
            if(fadeEffectRoutine != null)
                StopCoroutine(fadeEffectRoutine);
            fadeEffectRoutine = StartCoroutine(ShowRoutine(() => inputField.ActivateInputField()));
        }
    
        private void Hide()
        {
            inputField.DeactivateInputField(true);
            if(fadeEffectRoutine != null)
                StopCoroutine(fadeEffectRoutine);
            fadeEffectRoutine = StartCoroutine(HideRoutine(() => parentGameObject.SetActive(false)));
        }

        private IEnumerator ShowRoutine(Action onComplete = null)
        {
            var duration = fadeInOutDuration;
            var elapsed = 0f;
            
            while(elapsed <= duration)
            {
                canvasGroup.alpha = elapsed / duration;
                yield return null;
                elapsed += Time.deltaTime;
            }

            canvasGroup.alpha = 1f;
            onComplete?.Invoke();
        }

        private IEnumerator HideRoutine(Action onComplete = null)
        {
            var duration = fadeInOutDuration;
            var elapsed = 0f;
            
            while(elapsed <= duration)
            {
                canvasGroup.alpha = 1f - (elapsed / duration);
                yield return null;
                elapsed += Time.deltaTime;
            }

            canvasGroup.alpha = 0f;
            onComplete?.Invoke();
        }
        
        private void OnConsoleKeyPressed(InputAction.CallbackContext context)
        {
            if(!context.ReadValueAsButton())
                return;
            
            switch (isOpen)
            {
                case true:
                    Hide();
                    isOpen = false;
                    ConsoleToggledEvent?.Invoke(isOpen);
                    break;
                default:
                    Show();
                    isOpen = true;
                    ConsoleToggledEvent?.Invoke(isOpen);
                    break;
            }
        }
        
        private void OnAutoCompletePressed(InputAction.CallbackContext obj)
        {
            if(probableCommands.Count < 1)
                return;
            
            if (probableCommands.Count >= 1)
            {
                inputField.text = probableCommands.First();
                inputField.caretPosition = inputField.text.Length;
            }
        }
    
        private void OnInputFieldSelected(string arg)
        {
            inputField.placeholder.GetComponent<TMP_Text>().text = string.Empty;
        }
        
        private void OnInputFieldValueChanged(string arg)
        {
            probableCommands = commandManager.CommandList.Where(command => command.Contains(arg)).ToList();
        }

        private async void OnInputFieldSubmit(string arg)
        {
            if(string.IsNullOrEmpty(arg))
                return;

            var commandArgs = new CommandArgs(arg);
            
            inputField.text = string.Empty;
            inputField.ActivateInputField();
            
            if (commandManager.HasCommand(commandArgs.Command))
                await commandManager.RunCommand(commandArgs.Command, commandArgs.Parameters);
            else
            {
                Add($"unknown command : {commandArgs.Command}");
            }
        }
        
        private static string GetTimeStamp()
        {
            var time = DateTime.Now.TimeOfDay;
            return $"{time.Hours.ToString("00")}:{time.Minutes.ToString("00")}:{time.Seconds.ToString("00")}: ".Bold().Colorize("white");
        }
        
        private void OnDebugLogReceived(string log, string stacktrace, LogType type)
        {
            if(!showUnityConsoleLogs)
                return;
            
            switch (type)
            {
                case LogType.Error:
                    Add(log.Colorize("red"));
                    Add(stacktrace);
                    break;
                case LogType.Assert:
                    break;
                case LogType.Warning:
                    Add(log.Colorize("yellow"));
                    break;
                case LogType.Log:
                    Add(log);
                    break;
                case LogType.Exception:
                    Add(log.Colorize("red"));
                    Add(stacktrace.Colorize("red"));
                    break;
            }
        }

        #endregion
    }
}