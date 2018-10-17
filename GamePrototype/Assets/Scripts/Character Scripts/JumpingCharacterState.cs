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

        if (Input.GetAxisRaw("Horizontal") != 0.2 || Input.GetAxisRaw("Vertical") != 00 )
        {
            if (SubirPared.CanMove)
            {
                this.ToState(character, Character.Moving);
            }
        }
        else if (!character.IsGrounded && !character.IsInjured)
        {
            this.ToState(character, Character.Falling);
        }
        else if (character.IsInjured)
        {
            this.ToState(character, Character.Damage);
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