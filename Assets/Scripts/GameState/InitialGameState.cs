using Gameplay;
using Gameplay.Player;
using Service;
using UnityEngine;

namespace GameState
{
    public class InitialGameState : IGameState
    {
        public GameStateMachine stateMachine;

        public InitialGameState(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }

        public void Enter()
        {
            stateMachine.services.Single<ISceneLoader>().Load("SampleScene", OnLoaded);
        }

        private void OnLoaded()
        {
            Debug.Log("Loaded SampleScene");

            var player = stateMachine.services.Single<IFactory>().Create<Player>("Player");
            var camera = stateMachine.services.Single<IFactory>().Create<PlayerCamera>("PlayerCamera");

            camera.SetPlayer(player);
            
            NextState();
        }

        private void NextState() => stateMachine.Entry<PlayGameState>();

        public void Exit()
        {
        
        }
    }
}
