﻿namespace BehaviourStateMachine
{
    public interface IStateTransitionValidator<TStateType>  {

        bool Validate(TStateType from, TStateType to);
    }
}
