using System.Threading;
using Cysharp.Threading.Tasks;
using HK;
using UnityEngine;
using UnityEngine.InputSystem;

namespace DayTradeSim
{
    public class GameSceneController : MonoBehaviour
    {
        [SerializeField]
        private string debugCommand;

        private bool isGameEnd;

        private TinyStateMachine stateMachine;

        private string prompt;
        
        void Start()
        {
            BeginGameAsync(destroyCancellationToken).Forget();
        }
        
        private async UniTask BeginGameAsync(CancellationToken scope)
        {
            stateMachine = new TinyStateMachine();
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
            var result = await UniTask.WhenAny
            (
                UniTask.WaitWhile(() => !Keyboard.current.qKey.wasPressedThisFrame, cancellationToken: scope),
                UniTask.WaitWhile(() => !Keyboard.current.enterKey.wasPressedThisFrame, cancellationToken: scope)
            );
            prompt = result == 0 ? debugCommand : "TODO";
            Debug.Log("[State] Prompt End");
            stateMachine.Change(ProcessStateAsync);
        }
        
        private UniTask ProcessStateAsync(CancellationToken scope)
        {
            Debug.Log("[State] Process Begin");
            Debug.Log($"prompt = {prompt}");
            Debug.Log("[State] Process End");
            stateMachine.Change(PromptStateAsync);
            return UniTask.CompletedTask;
        }
    }
}
