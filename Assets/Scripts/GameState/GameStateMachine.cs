using System;
using System.Collections.Generic;
using Service;
using UnityEngine;

namespace GameState
{
    public class GameStateMachine
    {
        public IGameState Current { get; private set; }

        public Services services;

        private Dictionary<Type, IGameState> _states = new Dictionary<Type, IGameState>();

        public GameStateMachine(Services services)
        {
            this.services = services;

            _states.Add(typeof(InitialGameState), new InitialGameState(this));
            _states.Add(typeof(GameloopState), new GameloopState(this));
        }

        public void Entry<TState>() where TState: IGameState
        {
            if (Current is TState) return;

            if (_states.TryGetValue(typeof(TState), out var state))
                Entry(state);
            else
                Debug.LogError($"Not found GameState with type: {nameof(TState)}");
        }

        private void Entry(IGameState state)
        {
            Current?.Exit();
            Current = state;
            Current?.Enter();
        }
    }
}
