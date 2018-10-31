using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCharacterState : CharacterStateBase
{
    Vector2 direction;
    public GameObject PlayerForward = new GameObject();
    public Vector3 DireccionForward;

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
        //Rotar al jugador
        character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(DireccionForward), 0.15F);

        if (Input.GetButtonDown("Jump") && character.IsGrounded)
        {
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
        else if (character.IsGrounded)
        {
            this.ToState(character, Character.Grounded);
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

        DireccionForward.x = direction.x;
        DireccionForward.z = direction.y;
        DireccionForward.Normalize();
        DireccionForward = DireccionForward * 5;    //Conseguir la direccion hacia el que se esta moviendo

        if(DireccionForward != Vector3.zero)
        {
            PlayerForward.transform.position = character.transform.position + DireccionForward; //Actualizar la posicion de la direccion relativa al personaje
        }

        //character.transform.rotation = Quaternion.Slerp(character.transform.rotation, Quaternion.LookRotation(DireccionForward.normalized), 0.15F);
        Debug.DrawLine(PlayerForward.transform.position, character.transform.position, Color.red);
    }
}