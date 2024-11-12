using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using HK;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using UnitySequencerSystem;

namespace DayTradeSim
{
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private UIDocument uiDocumentPrefab;
        
        [SerializeField]
        private CommandDataList.DictionaryList commandDataList;
        
        private bool isGameEnd;

        private TinyStateMachine stateMachine;
        
        private UIDocument uiDocument;
        
        private TextField promptTextField;

        private string prompt;
        
        private Queue<string> queuePrompts = new();
        
        void Start()
        {
            BeginGameAsync(destroyCancellationToken).Forget();
        }
        
        private async UniTask BeginGameAsync(CancellationToken scope)
        {
            stateMachine = new TinyStateMachine();
            uiDocument = Instantiate(uiDocumentPrefab);
            promptTextField = uiDocument.rootVisualElement.Q<TextField>("TextField");
            stateMachine.Change(PromptStateAsync);
            await UniTask.WaitWhile(() => !isGameEnd, cancellationToken: scope);
            stateMachine.Dispose();
            Debug.Log("Game End");
        }
        
        private async UniTask LoginStateAsync(CancellationToken scope)
        {
            Debug.Log("[State] Login Begin");
            await UniTask.WaitWhile(() => !Keyboard.current.anyKey.wasPressedThisFrame, cancellationToken: scope);
            Debug.Log("[State] Login End");
            stateMachine.Change(PromptStateAsync);
        }
        
        private async UniTask PromptStateAsync(CancellationToken scope)
        {
            Debug.Log("[State] Prompt Begin");
            if (queuePrompts.Count > 0)
            {
                prompt = queuePrompts.Dequeue();
            }
            else
            {
                promptTextField.Focus();
                await UniTask.WaitWhile(() => !Keyboard.current.enterKey.wasPressedThisFrame, cancellationToken: scope);
                prompt = promptTextField.value;
                promptTextField.value = "";
            }
            Debug.Log("[State] Prompt End");
            stateMachine.Change(ProcessStateAsync);
        }
        
        private async UniTask ProcessStateAsync(CancellationToken scope)
        {
            Debug.Log("[State] Process Begin");
            Debug.Log($"prompt = {prompt}");
            if (string.IsNullOrEmpty(prompt))
            {
                stateMachine.Change(PromptStateAsync);
                return;
            }
            BeginQueuePromptAsync(scope).Forget();
            var data = Regex.Matches(prompt, "\\\"(.*?)\\\"|\\S+")
                .Select(x => x.Groups[0].Value.Replace("\"", ""))
                .ToList();
            if (commandDataList.TryGetValue(data[0], out var command))
            {
                var container = new Container();
                container.Register("Data", data);
                var sequencer = new Sequencer(container, command.Sequences);
                await sequencer.PlayAsync(scope);
            }
            else
            {
                Debug.Log($"Command not found: {data[0]}");
            }
            Debug.Log("[State] Process End");
            stateMachine.Change(PromptStateAsync);
        }
        
        private async UniTask BeginQueuePromptAsync(CancellationToken scope)
        {
            try
            {
                while (true)
                {
                    promptTextField.Focus();
                    await UniTask.WaitWhile(() => !Keyboard.current.enterKey.wasPressedThisFrame, cancellationToken: scope);
                    queuePrompts.Enqueue(promptTextField.value);
                    promptTextField.value = "";
                    await UniTask.NextFrame();
                }
            }
            catch (OperationCanceledException)
            {
                Debug.Log("End BeginQueuePromptAsync");
            }
        }
    }
}
