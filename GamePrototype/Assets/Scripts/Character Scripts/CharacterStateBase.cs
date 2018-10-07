using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterStateBase : ICharacterState
{
    public virtual void OnEnter(Character character) { }

    public virtual void OnExit(Character character) { }

    public virtual void ToState(Character character, ICharacterState targetState)
    {
        character.State.OnExit(character);
        character.State = targetState;
        character.State.OnEnter(character);
    }

    public abstract void FixedUpdate(Character character);

    public abstract void HandleInput(Character character);
    
}