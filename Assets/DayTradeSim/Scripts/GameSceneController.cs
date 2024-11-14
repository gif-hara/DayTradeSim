using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Cysharp.Threading.Tasks;
using DayTradeSim.StockSimulator;
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
        private CompanyGenerator companyGenerator;

        [SerializeField]
        private int initialCompanyNumber;
        
        [SerializeField]
        private CommandDataList.DictionaryList commandDataList;
        
        private bool isGameEnd;

        private TinyStateMachine stateMachine;
        
        private UIDocument uiDocument;
        
        private TextField promptTextField;

        private string prompt;
        
        private readonly Queue<string> queuePrompts = new();
        
        StockSimulator.Core stockSimulator;
        
        void Start()
        {
            stockSimulator = new StockSimulator.Core(companyGenerator, initialCompanyNumber);
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
            await UniTask.WaitWhile(() => !Keyboard.current.anyKey.wasPressedThisFrame, cancellationToken: scope);
            stateMachine.Change(PromptStateAsync);
        }
        
        private async UniTask PromptStateAsync(CancellationToken scope)
        {
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
            stateMachine.Change(ProcessStateAsync);
        }
        
        private async UniTask ProcessStateAsync(CancellationToken scope)
        {
            if (string.IsNullOrEmpty(prompt))
            {
                stateMachine.Change(PromptStateAsync);
                return;
            }
            BeginQueuePromptAsync(scope).Forget();
            var commandLine = new CommandLine(prompt);
            if (commandDataList.TryGetValue(commandLine.GetCommandName(), out var command))
            {
                var container = new Container();
                container.Register(commandLine);
                container.Register(stockSimulator);
                var sequencer = new Sequencer(container, command.Sequences);
                await sequencer.PlayAsync(scope);
            }
            else
            {
                Debug.Log($"Command not found: {commandLine.GetCommandName()}");
            }
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
                    await UniTask.NextFrame(cancellationToken: scope);
                }
            }
            catch (OperationCanceledException)
            {
            }
            catch (Exception e)
            {
                Debug.LogException(e);
            }
        }
    }
}
