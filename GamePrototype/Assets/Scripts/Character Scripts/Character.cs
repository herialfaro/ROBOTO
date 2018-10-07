using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float moveSpeed;
    /// <summary>
    /// 
    /// </summary>
    public const float JUMP_HEIGHT = 2;
    public const float JUMP_TIME_1 = .8f;
    public const float JUMP_TIME_2 = .6f;

    /* 
     * Gravity based on this formula   G= -8h/t(total)
     * Maximum altitude                h= Vo/2g
     * Time to get the top             t(Top) = Vo/g
     * Total time in air               t(total) = usually this would be 2t(Top), but in this particular case, we want our player to fall faster than it rises
    */
    private const float JUMP_FORCE = (4 * JUMP_HEIGHT) / JUMP_TIME_1;
    public const float GRAVITY_RISING = (-8 * JUMP_HEIGHT) / (JUMP_TIME_1 * JUMP_TIME_1);
    private const float GRAVITY_FALLING = (-8 * JUMP_HEIGHT) / (JUMP_TIME_2 * JUMP_TIME_2);


    public static float verticalMomentum;
    private float horizontalMomentum;
    private float horizontalMovementZ;

    private Vector3 movementVector;

    public ICharacterState state;
    private CharacterController flyBoy;

    public static CharacterStateBase Jumping;
    public static CharacterStateBase Grounded;
    public static CharacterStateBase Falling;
    public static CharacterStateBase Moving;

    public static bool isGrounded = true;

    // este bool es para verificar si el personaje hizo un double jump 
    public bool isDoubleJump = false;

    public bool isWalled;

    public ICharacterState State
    {
        get
        {
            return this.state;
        }
        set
        {
            this.state = value;
        }
    }

    public CharacterController Flyboy
    {
        get
        {
            return flyBoy;
        }

        set
        {
            flyBoy = value;
        }
    }

    public float VerticalMomentum
    {
        get
        {
            return verticalMomentum;
        }

        set
        {
            verticalMomentum = value;
        }
    }
    public float HorizontalMomentum
    {
        get
        {
            return horizontalMomentum;
        }

        set
        {
            horizontalMomentum = value;
        }
    }

    public float GRAVITY_FALLING1
    {
        get
        {
            return GRAVITY_FALLING;
        }
    }

    public float JUMP_FORCE1
    {
        get
        {
            return JUMP_FORCE;
        }
    }

    public bool IsGrounded
    {
        get
        {
            return isGrounded;
        }
        private set
        {
            isGrounded = value;
        }
    }
    /*todo lo de abajo es para que se pueda 
     usar el "isDoubleJump" en otras partes del
     codigo */
    public bool IsDoubleJump
    {
        get
        {
            return isDoubleJump;
        }
        set
        {
           isDoubleJump = value;
        }
    }

    public float HorizontalMovementZ
    {
        get
        {
            return horizontalMovementZ;
        }

        set
        {
            horizontalMovementZ = value;
        }
    }

    bool CursorLockedVar;

    private void Awake()
    {
        Physics.gravity = new Vector3(0, GRAVITY_FALLING, 0);

        Grounded = new GroundedCharacterState();
        Jumping = new JumpingCharacterState();
        Falling = new FallingCharacterState();
        Moving = new MovingCharacterState();
    }

    private void Start()
    {
        flyBoy = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = (false);
        CursorLockedVar = (true);

        state = Grounded;

    }
    public void Update()
    {
        //transform.rotation.y = Camera.main.transform.rotation.y;
        //Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //transform.LookAt(Camera.main.transform.rotation);


        if (Input.GetKeyDown("escape") && !CursorLockedVar)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = (false);
            CursorLockedVar = (true);
        }
        else if (Input.GetKeyDown("escape") && CursorLockedVar)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = (true);
            CursorLockedVar = (false);
        }

        //if(Input.GetAxisRaw("Mouse X") > .2f || Input.GetAxisRaw("Mouse X") < -.2f)
        //{
        //    var CharacterRotation = Camera.main.transform.rotation;
        //    CharacterRotation.x = 0;
        //    CharacterRotation.z = 0;
        //    transform.rotation = CharacterRotation;
        //   // AxisDirection();
        //}

        //AxisDirection();

        CheckGrounded();
    }
    public void FixedUpdate()
    {
        this.State.FixedUpdate(this);
        HandleMovement();
    }

    private void CheckGrounded()
    {
        RaycastHit rayHit;
        float characterHeight = flyBoy.height;
        float characterWidth = flyBoy.radius;
        Vector3 startPoint = transform.position;
        IsGrounded = Physics.SphereCast(startPoint, characterWidth / 2, Vector3.down, out rayHit, characterHeight / 2);
    }

    /*private void CheckWalled()
    {
        RaycastHit rayHit;
        float characterHeight = flyBoy.height;
        float charaterWidth = flyBoy.radius;
        Vector3 startPoint = transform.position;
        isWalled = Physics.SphereCast(startPoint, charaterWidth / 2, lookDirection, out rayHit, characterHeight / 2);
    }*/

    public void HandleInput()
    {
        this.State.HandleInput(this);
    }

    public void HandleMovement()
    {
        movementVector.y = verticalMomentum;
        movementVector.x = horizontalMomentum;
        movementVector.z = horizontalMovementZ;

        transform.TransformDirection(movementVector);
        flyBoy.Move(movementVector * Time.deltaTime);
    }
}