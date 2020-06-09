using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamaraMouse : MonoBehaviour {

    private struct target
    {
        public GameObject lockable_target;
        public float distance_target;
        public bool isLockable;
        public bool isOpen;
    }

    public static bool SelectStateEnabled;

    public static bool Lookforobjects;
    public GameObject objective;
    private GameObject pivot;
    public static GameObject lockOnTarget;
    public static GameObject[] lockable_objects;

    public float VelRotacion; //5f DEFAULT
    public float VelTranslacion;
    private float RawVelocity;
    public float lockOnRadius; //25 DEFAULT
    public float lockOnDistance; //4f DEFAULT

    private Vector3 Distancia;
    private Vector3 Robotos_target;
    public static bool state_Locked; //Determina el estado de Lock-on : Activado o desactivado
    private target[] target_list;
    private int target_counter;

    //Variables de Colisiones
    Vector3 Vector;
    Vector3 highVector;
    float plusY = .1f;
    float Cam_PlayerDistance;

    private void Awake()
    {
        pivot = new GameObject();
        this.transform.position = new Vector3(objective.transform.position.x + 0f, objective.transform.position.y + -1.0f, objective.transform.position.z + 7f);
        state_Locked = false;
        Lookforobjects = true;
        SelectStateEnabled = true;
        target_counter = 0;
        Distancia = transform.position - objective.transform.position;
        RawVelocity = VelRotacion;
    }

    private void Start()
    {
        pivot.transform.position = objective.transform.position;
        pivot.transform.parent = objective.transform;
    }

    private void Update()
    {
        if(Lookforobjects)
        {
            FindLockableObjects();
            Lookforobjects = false;
        }
    }

    // Update is called once per frame
    private void LateUpdate() {

        for (int i = 0; i < target_list.Length; i++)
        {
            float fDistance = Vector3.Distance(target_list[i].lockable_target.transform.position, objective.transform.position);
            target_list[i].distance_target = fDistance;
            if (fDistance <= lockOnRadius)
            {
                target_list[i].isLockable = true;
            }
            else
            {
                target_list[i].isLockable = false;
                target_list[i].isOpen = true;
            }
        }
        SelectState();
        //Colision
        Vector = objective.transform.position - transform.position;
        Cam_PlayerDistance = Mathf.Sqrt(Mathf.Pow(Vector.x, 2) + Mathf.Pow(Vector.y, 2) + Mathf.Pow(Vector.z, 2));
        RaycastHit hit;
        //if (Physics.Raycast(objective.transform.position, transform.position, Cam_PlayerDistance))
        if (Physics.Raycast(objective.transform.position, transform.position, out hit, Cam_PlayerDistance))
        {
            highVector = hit.point;
            highVector.y = hit.point.y + 5;
            //transform.position = Vector3.Lerp(transform.position, highVector, plusY);
            transform.position = Vector3.Lerp(transform.position, hit.point, plusY);    //AQUI PRRO ******************************
            if (plusY < 1)
            {
                plusY += .1f;
            }
            //Debug.Log("ALV PRRO");
        }
        else
        {
            plusY = .1f;
        }
        Debug.DrawLine(objective.transform.position, transform.position, Color.red);
        transform.LookAt(objective.transform);
    }

    private void CameraRotation()
    {
        var MoveX = 0f;
        var MoveY = 0f;
        if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
        {
            MoveX = Input.GetAxis("Mouse X");
            MoveY = Input.GetAxis("Mouse Y");
            VelRotacion = RawVelocity;
        }
        else if (Input.GetAxis("Joystick X") != 0 || Input.GetAxis("Joystick Y") != 0)
        {
            MoveX = Input.GetAxis("Joystick X");
            MoveY = Input.GetAxis("Joystick Y");
            VelRotacion = RawVelocity * 30;
        }

        //if (MouseX != 0)
        //{
        float horizontal = MoveX * VelRotacion * Time.deltaTime;
        pivot.transform.Rotate(0.0f, horizontal, 0.0f);
        //}

        //if (MouseY != 0)
        //{
        /*float vertical = MouseY * VelRotacion * Time.deltaTime;
        pivot.transform.Rotate(0.0f, 0.0f, 0.0f);*/
        //}

        float angleX = pivot.transform.eulerAngles.x;
        float angleY = pivot.transform.eulerAngles.y;

        Quaternion rotation = Quaternion.Euler(angleX, angleY, 0.0f);
        transform.position = pivot.transform.position - (rotation * Distancia);

        transform.LookAt(pivot.transform);

        //horizontal = 0;
    }

    private void FindLockableObjects()
    {
        lockable_objects = GameObject.FindGameObjectsWithTag("Lockable");
        if (lockable_objects.Length > 0)
        {
            target_list = new target[lockable_objects.Length];
            for (int i = 0; i < lockable_objects.Length; i++)
            {
                target new_target = new target
                {
                    lockable_target = lockable_objects[i],
                    isLockable = false,
                    isOpen = true
                };
                target_list[i] = new_target;
            }
        }
    }

    private void SelectState()
    {
        if (state_Locked)
        {
            if (Input.GetButtonDown("Target") && SelectStateEnabled)
            {
                state_Locked = false;
                target_counter = 0;
                for (int i = 0; i < target_list.Length; i++)
                {
                    target_list[i].isOpen = true;
                }
            }

            if (Input.GetMouseButtonDown(2) || Input.GetAxis("Joystick X") > 0)
            {
                //Code for action on mouse moving right
                if (target_counter > 0)
                {
                    for (int i = 0; i < target_list.Length; i++)
                    {
                        if (target_list[i].isLockable && target_list[i].isOpen)
                        {
                            lockOnTarget = target_list[i].lockable_target;
                            target_list[i].isOpen = false;
                            target_counter--;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < target_list.Length; i++)
                    {
                        if (target_list[i].isLockable)
                        {
                            target_list[i].isOpen = true;
                            target_counter++;
                        }
                    }
                }
            }

            if(Vector3.Distance(lockOnTarget.transform.position,objective.transform.position) <= lockOnRadius)
            {
                SelectTarget();
                Robotos_target = new Vector3(lockOnTarget.transform.position.x, objective.transform.position.y, lockOnTarget.transform.position.z);
                objective.transform.LookAt(Robotos_target);
            }
            else { state_Locked = false;
            Target_arrow.arrow_plane.gameObject.SetActive(false);
                objective.gameObject.GetComponent<WhipBase>().ReturnHook();
            }
        }
        else
        {
            if (Input.GetButtonDown("Target") && target_list.Length > 0)
            {
                lockOnTarget = target_list[0].lockable_target;
                int last_check = 0;
                for (int i = 0; i < target_list.Length; i++)
                {
                    if (target_list[i].isLockable && target_list[i].isOpen)
                    {
                        if (target_list[i].distance_target < Vector3.Distance(lockOnTarget.transform.position, objective.transform.position))
                        {
                            lockOnTarget = target_list[i].lockable_target;
                            
                            target_list[i].isOpen = false;
                            target_list[last_check].isOpen = true;
                            last_check = i;
                        }
                        ++target_counter;
                    }
                }
                if (target_counter > 0)
                {
                    state_Locked = true;
                }
            }
            CameraRotation();
        }
    }

    private void SelectTarget ()
    {
        Vector3 direction;
        if (lockOnTarget.transform.position.x > objective.transform.position.x)
        {
            if (lockOnTarget.transform.position.z > objective.transform.position.z)
            {
                direction = new Vector3(objective.transform.position.x - lockOnDistance,
                    objective.transform.position.y, objective.transform.position.z - lockOnDistance);
            }
            else
            {
                direction = new Vector3(objective.transform.position.x - lockOnDistance,
                    objective.transform.position.y, objective.transform.position.z + lockOnDistance);
            }
        }
        else
        {
            if (lockOnTarget.transform.position.z > objective.transform.position.z)
            {
                direction = new Vector3(objective.transform.position.x + lockOnDistance,
                    objective.transform.position.y, objective.transform.position.z - lockOnDistance);
            }
            else
            {
                direction = new Vector3(objective.transform.position.x + lockOnDistance,
                    objective.transform.position.y, objective.transform.position.z + lockOnDistance);
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, direction, Time.deltaTime * VelTranslacion);
        transform.LookAt(lockOnTarget.transform);
    }
}
