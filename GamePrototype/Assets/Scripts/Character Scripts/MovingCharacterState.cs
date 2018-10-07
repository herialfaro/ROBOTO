using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacterState : CharacterStateBase
{
    Vector2 direction;

    public override void HandleInput(Character character)
    {
    }

    public override void OnEnter(Character character)
    {
        direction.x = Input.GetAxisRaw("Horizontal");
        direction.y = Input.GetAxisRaw("Vertical");
    }

    public override void FixedUpdate(Character character)
    {
        Debug.Log("Estado Move");
        HandleMoving(character);

        /* este if verifica si el personaje esta en el piso 
          y luego lo deja saltar , con el boton que definimos como "Jump" */
        if (Input.GetButtonDown("Jump") && character.IsGrounded)
        {
            // decimos que el personaje puede hacer el doble salto 
           character.isDoubleJump = false;
            
            // esto es para que yo vea monitorear los resultados  
            Debug.Log("Estado Moving Jump [" + character.isDoubleJump + ']');

            //esto es para iniciar el salto .
           this.ToState(character, Character.Jumping);
        }
        else if (!character.IsGrounded)
        {
            this.ToState(character, Character.Falling);
        }
        else if (Input.GetAxisRaw("Horizontal") != 0.2 || Input.GetAxisRaw("Vertical") != 0)
        {
            this.ToState(character, Character.Moving);
        }
    }

    public static Vector2 Rotate(Vector2 v, float degrees)
    {
        float sin = Mathf.Sin(degrees * Mathf.Deg2Rad);
        float cos = Mathf.Cos(degrees * Mathf.Deg2Rad);

        float tx = v.x;
        float ty = v.y;
        v.x = (cos * tx) - (sin * ty);
        v.y = (sin * tx) + (cos * ty);
        return v;
    }

    public void HandleMoving(Character character)
    {
        float rotation = character.transform.rotation.eulerAngles.y;
        direction = Rotate(direction, -rotation);
        character.HorizontalMomentum = direction.x * character.moveSpeed;
        character.HorizontalMovementZ = direction.y * character.moveSpeed;
    }
}