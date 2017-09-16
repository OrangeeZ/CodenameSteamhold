﻿using System;
using System.Diagnostics;
using Assets.Scripts.Extensions;
using UniRx;
using Debug = UnityEngine.Debug;

namespace BehaviourStateMachine
{
    public class StateManager<TStateType> : IStateManager<TStateType>
    {
        private readonly Stopwatch _stopwatch;
        protected IStateTransitionValidator<TStateType> _validator;
        private readonly IStateController<TStateType> _stateController;
        protected IStateMachine _stateMachine;
        private readonly IStateFactory<TStateType> _stateFactory;
        protected readonly IDisposable _controllerDisposable;

        #region constructor

        public StateManager(IStateController<TStateType> stateController,
            IStateMachine stateMachine,
            IStateFactory<TStateType> stateFactory,
            IStateTransitionValidator<TStateType> validator = null)
        {
            _stateController = stateController;
            _stateMachine = stateMachine;
            _stateFactory = stateFactory;
            _validator = validator;
            _stopwatch = new Stopwatch();
            _controllerDisposable = 
                _stateController.StateObservable.Skip(1)
                .Subscribe(ExecuteState);
        }

        #endregion

        #region public properties


        public TStateType CurrentState { get; protected set; }


        public TStateType PreviousState { get; protected set; }


        #endregion

        #region public methods

        public virtual void Dispose()
        {
            _stateMachine.Dispose();
            _stopwatch.Stop();
            _stopwatch.Reset();
            _controllerDisposable.Cancel();
        }

        public void SetState(TStateType state)
        {
            _stateController.SetState(state);
        }

        #endregion

        #region private methods
        
        protected void ExecuteState(TStateType state)
        {
            if (!ValidateTransition(state))
                return;
            PreviousState = CurrentState;
            CurrentState = state;
            OnStateChanged(PreviousState, CurrentState);
            ChangeState(state);
        }

        protected virtual void ChangeState(TStateType state)
        {
            var stateBehaviour = _stateFactory.Create(state);
            _stateMachine.Execute(stateBehaviour);
        }

        protected virtual bool ValidateTransition(TStateType nextState)
        {
            return _validator == null || 
                _validator.Validate(CurrentState, nextState);
        }


        private void OnStateChanged(TStateType fromState, TStateType toState)
        {
            _stopwatch.Stop();
            Debug.LogFormat("From State {0} To State {1} xecution finished. RESULT TIME {2}",
                fromState, toState, _stopwatch.ElapsedMilliseconds);
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        #endregion

    }
}
