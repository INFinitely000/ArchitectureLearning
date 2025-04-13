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
            var ui = stateMachine.services.Single<IGameFactory>().CreateUI();

            camera.SetPlayer(player);
            player.Construct(stateMachine.services.Single<IInput>());
            
            ui.PlayerHealth.SetHealth(player.Health);
            
            player.Health.SetHealth( stateMachine.services.Single<IGameData>().Player.health );
            player.Health.SetMaxHealth( stateMachine.services.Single<IGameData>().Player.maxHealth );
            
            NextState();
        }

        private void NextState() => stateMachine.Entry<GameloopState>();

        public void Exit()
        {

        }
    }
}
