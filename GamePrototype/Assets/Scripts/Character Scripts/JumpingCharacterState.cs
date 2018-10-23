using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingCharacterState : CharacterStateBase
{
    public override void HandleInput(Character character)
    {
    }

    public override void FixedUpdate(Character character)
    {
        Debug.Log("Estado Jump");
        HandleJump(character);

        Debug.Log("Estado JUMPING Jump [" + character.isDoubleJump + ']');

        if (character.IsGrounded)
        {
            character.isDoubleJump = false;
        }

        if (Input.GetAxisRaw("Horizontal") != 0.2 || Input.GetAxisRaw("Vertical") != 00)
        {
            Debug.Log("Entrando al estado MOVING ");
            this.ToState(character, Character.Moving);
        }
        else if (!character.IsGrounded)
        {
            Debug.Log("Entrando al estado Falling ");
            this.ToState(character, Character.Falling);
        }

    }

    public void HandleJump(Character character)
    {
        character.VerticalMomentum = character.JUMP_FORCE1;
    }

    public override void OnEnter(Character character)
    {
    }
}