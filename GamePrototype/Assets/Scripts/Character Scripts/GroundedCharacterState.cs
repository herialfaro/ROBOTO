using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedCharacterState : CharacterStateBase
{
    public override void HandleInput(Character character)
    {
    }

    public override void OnEnter(Character character)
    {
    }

    public override void FixedUpdate(Character character)
    {
        Debug.Log("Estado Ground");
        HandleGround(character);

        
        if (Input.GetButtonDown("Jump") && character.IsGrounded)
        {
            this.ToState(character, Character.Jumping);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0.2 || Input.GetAxisRaw("Vertical") != 0)
        {
            this.ToState(character, Character.Moving);
        }
        else if (!character.IsGrounded)
        {
            this.ToState(character, Character.Falling);
        }
    }
    private void HandleGround(Character character)
    {
        character.VerticalMomentum = 0;
        character.HorizontalMomentum = 0;
    }
}