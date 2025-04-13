using System;
using Gameplay;
using Service;
using UnityEngine;

namespace GameState
{
    public class GameloopState : IGameState
    {
        public GameStateMachine stateMachine;

        private GameloopProvider _provider;
        
        public GameloopState(GameStateMachine stateMachine)
        {
            this.stateMachine = stateMachine;
        }
        
        public void Enter()
        {
            _provider = stateMachine.services.Single<IGameFactory>().CreateEmpty<GameloopProvider>();
            _provider.Construct(Update);
        }

        public void Exit()
        {
            if (_provider) 
                UnityEngine.Object.Destroy(_provider);
        }

        private void Update()
        {
            if (stateMachine.services.Single<IInput>().IsRestart)
                stateMachine.Entry<InitialGameState>();
        }
    }
}