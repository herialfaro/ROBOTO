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
        character.HorizontalMomentum = 0;
        character.HorizontalMovementZ = 0;

        HandleDamage(character);
        
        if (Input.GetButtonDown("Jump") && character.IsGrounded && !character.IsInjured)
        {
            this.ToState(character, Character.Jumping);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0.2 && !character.IsInjured || Input.GetAxisRaw("Vertical") != 0 && !character.IsInjured)
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
        else if (Character.isInjured)
        {
            this.ToState(character, Character.Damage);
        }
    }
    
    public void HandleDamage(Character character)
    {
        if (character.IsInjured)
        {
            character.CanBeHurt = false;
        }
        else
        {
            character.CanBeHurt = true;
            Character.isInjured = false;
            Debug.Log("Can Be hurt");
        }
    }
    
}
