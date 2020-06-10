using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class FallingCharacterState : CharacterStateBase
{
    bool DoubleJump = false;

    public override void HandleInput(Character character)
    {
    }


    public override void OnEnter(Character character)
    {
    }

    public override void FixedUpdate(Character character)
    {
        Gravity(character);

        if (character.IsGrounded)
        {
            this.ToState(character, Character.Grounded);
            DoubleJump = false;
        }
        if (Input.GetAxisRaw("Horizontal") != 0.2 || Input.GetAxisRaw("Vertical") != 0)
        {
            if (SubirPared.CanMove)
            {
                this.ToState(character, Character.Moving);
            }
        }
    }

    private void Gravity(Character character)
    {
        character.VerticalMomentum += character.GRAVITY_FALLING1 * Time.fixedDeltaTime;
    }
}