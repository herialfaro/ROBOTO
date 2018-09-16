using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterState {
    void OnEnter(Character character);

    void OnExit(Character character);

    void ToState(Character character, ICharacterState targetState);

    void FixedUpdate(Character character);

    void HandleInput(Character character);
}