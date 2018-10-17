using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCharacterState : CharacterStateBase
{

    public override void HandleInput(Character character)
    {
    }

    public override void OnEnter(Character character)
    {
        
    }

    public override void FixedUpdate(Character character)
    {
        Debug.Log("Estado Herido");
        
        if (Input.GetButtonDown("Jump") && character.IsGrounded && !character.IsInjured)
        {
            this.ToState(character, Character.Jumping);
        }
        else if (Character.isInjured)
        {
            this.ToState(character, Character.Damage);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0.2 || Input.GetAxisRaw("Vertical") != 0)
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
    }
    
}
