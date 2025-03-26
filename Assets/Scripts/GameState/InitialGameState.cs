using Data;
using Gameplay;
using Gameplay.MainPlayer;
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

            var player = stateMachine.services.Single<IGameFactory>().CreatePlayer();
            var camera = stateMachine.services.Single<IGameFactory>().CreatePlayerCamera();

            camera.SetPlayer(player);
            
            player.Health.SetHealth( stateMachine.services.Single<IGameData>().player.health );
            player.Health.SetMaxHealth( stateMachine.services.Single<IGameData>().player.maxHealth );
            
            NextState();
        }

        private void NextState() => stateMachine.Entry<PlayGameState>();

        public void Exit()
        {
        
        }
    }
}
