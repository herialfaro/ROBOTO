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
        Debug.Log("Estado FALLING Jump [" + character.isDoubleJump + ']');

        /*Todo lo que hace esto es Verificar 
         * si el personaje esta 
         * en el suelo */

        if (character.IsGrounded)
        {
            character.isDoubleJump = false;
        }
        if (Input.GetAxisRaw("Horizontal") != 0.2 || Input.GetAxisRaw("Vertical") != 0)
        {
            this.ToState(character, Character.Moving);
        }
        // este if verifica si el presonaje puede hacer el double salto
        else if (Input.GetButtonDown("Jump") && character.IsDoubleJump == false)
        {
            //dicimos que el presonaje hizo el doble salto 
            character.IsDoubleJump = true;

            // el presonaje hace el salto 
            this.ToState(character, Character.Jumping);
   
        }
    }

    private void Gravity(Character character)
    {
        character.VerticalMomentum += character.GRAVITY_FALLING1 * Time.fixedDeltaTime;
    }
}