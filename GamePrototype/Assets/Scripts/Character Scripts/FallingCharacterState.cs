using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingCharacterState : CharacterStateBase
{
    public override void HandleInput(Character character)
    {
    }

    public override void OnEnter(Character character)
    {
    }

    public override void FixedUpdate(Character character)
    {
        Debug.Log("Estado Falling");
        Gravity(character);
        if (Input.GetAxisRaw("Horizontal") != 0.2 || Input.GetAxisRaw("Vertical") != 0)
        {
            this.ToState(character, Character.Moving);
        }
        else if (character.IsGrounded)
        {
            this.ToState(character, Character.Grounded);
        }
    }

    private void Gravity(Character character)
    {
        character.VerticalMomentum += character.GRAVITY_FALLING1 * Time.fixedDeltaTime;
    }
}