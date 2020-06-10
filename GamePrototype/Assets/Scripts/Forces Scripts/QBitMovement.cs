using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QBitMovement : MonoBehaviour
{
    public GameObject objective;
    private GameObject pivot;
    private GameObject home;

    public static bool isAttacked;
    private bool changeHome;
    private float randomRange;

    public ushort QBitType; //0ther-STATIC 1-AGGRESIVE 2-CALM 3-TIMID 4-VIOLENT 5-COWARD
    private string actualstate; //WANDER, SEEK, FLEE, STOP

    public float moveSpeed;
    public float runSpeed;
    public float approachradius;
    public float homeradius;
    public float warningradius;
    public float displacementmargin; //VALUE FROM 0 TO 2
    public float wanderprobability; //VALUE FROM 0 TO 1

    private const float VISIONANGLE = Mathf.PI / 2;
    private const float GRAVITY = 20.0f;
    private const float GRAVITYWHENFALLING = 25.0f;

    private Vector3 steering;
    private Vector3 velocity;
    private CharacterController QBitController;
    private SteeringManager manager;

    private int UpdateCounter = 0;

    private void Awake()
    {
        home = new GameObject();
        pivot = new GameObject();
        home.transform.position = transform.position;
        pivot.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 1.5f);
        manager = new SteeringManager();
        steering = new Vector3(0.0f, 0.0f, 0.0f);
        velocity = new Vector3(0.0f, 0.0f, 0.0f);
    }

    // Use this for initialization
    private void Start()
    {
        QBitController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    private void Update()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        randomRange = Random.Range(0.0f, 1.0f);

        if(UpdateCounter > 12)
        {
            QBitState();
            Locomotion();
            UpdateCounter = 0;
        }
        UpdateCounter++;
    }

    private void QBitState()
    {
        switch(QBitType)
        {
            case 1:
                if (Vector3.Distance(transform.position, objective.transform.position) < warningradius)
                {
                    changeHome = true;
                    actualstate = "SEEK";
                }
                else
                {
                    if(randomRange > wanderprobability)
                    {
                        actualstate = "STOP";
                    }
                    else
                    {
                        if (changeHome)
                        {
                            home.transform.position = transform.position;
                            changeHome = false;
                        }
                        actualstate = "WANDER";
                    }
                }
                    break;
            case 2:
                if (isAttacked)
                {
                    changeHome = true;
                    QBitType = 1;
                }
                else
                {
                    if (randomRange > wanderprobability)
                    {
                        actualstate = "STOP";
                    }
                    else
                    {
                        if (changeHome)
                        {
                            home.transform.position = transform.position;
                            changeHome = false;
                        }
                        actualstate = "WANDER";
                    }
                }
                break;
            case 3:
                if (Vector3.Distance(transform.position, objective.transform.position) < warningradius)
                {
                    changeHome = true;
                    actualstate = "FLEE";
                }
                else
                {
                    if (randomRange > wanderprobability)
                    {
                        actualstate = "STOP";
                    }
                    else
                    {
                        if (changeHome)
                        {
                            home.transform.position = transform.position;
                            changeHome = false;
                        }
                        actualstate = "WANDER";
                    }
                }
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                actualstate = "STOP";
                break;
        }
    }

    private void Locomotion()
    {
        velocity = manager.Truncate(velocity + steering, moveSpeed);

        //QBIT BEHAVIOR STATE MACHINE

        switch (actualstate)
        {
            case "WANDER":
                Vector3 wander = new Vector3(0.0f, 0.0f, 0.0f);
                if (Vector3.Distance(transform.position, home.transform.position) > homeradius)
                {
                    Vector3 seek_wander = manager.Seek(transform.position, home.transform.position, moveSpeed);
                    wander = manager.Wander(transform.position, seek_wander, moveSpeed, VISIONANGLE, homeradius, displacementmargin);
                    wander += velocity;
                }
                else
                {
                    if (QBitController.velocity.magnitude > 0.1f)
                    {
                        wander = manager.Wander(transform.position, pivot.transform.position, moveSpeed, VISIONANGLE, homeradius, displacementmargin);
                    }
                    else
                    {
                        Vector3 flee_wander = manager.Flee(transform.position, pivot.transform.position, moveSpeed, homeradius);
                        wander = manager.Wander(transform.position, flee_wander, moveSpeed, VISIONANGLE, homeradius, displacementmargin);
                    }
                }
                steering = wander;
                pivot.transform.position = steering;
                break;
            case "SEEK":
                if (Vector3.Distance(transform.position, objective.transform.position) > approachradius)
                {
                    Vector3 seek = manager.Seek(transform.position, objective.transform.position, moveSpeed);
                    steering = steering + seek;
                }
                else
                {
                    Vector3 arrive = manager.Arrive(transform.position, objective.transform.position, runSpeed, approachradius);
                    steering = velocity - arrive;
                }
                break;
            case "FLEE":
                Vector3 flee = manager.Flee(transform.position, objective.transform.position, moveSpeed, warningradius);
                if (QBitController.velocity.magnitude > 0.1f)
                {
                    steering = (steering + flee)/1.2f;
                }
                else
                {
                    steering = flee;
                }
                break;
            case "STOP":
                steering.Set(0.0f, 0.0f, 0.0f);
                break;
            default:
                break;
        }

        //QBIT BEHAVIOR STATE MACHINE

        steering = new Vector3(steering.x, 0.0f, steering.z);

        //if (!QBitController.isGrounded)
        //{
        //    steering.y -= (GRAVITY * Time.deltaTime);
        //}
        steering = transform.TransformDirection(steering);
        QBitController.Move(steering * Time.fixedDeltaTime);
    }
}
