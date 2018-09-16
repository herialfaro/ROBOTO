using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//GRAPLING HOOK CODE BY Sykoo on Youtube //

public class WhipBase : MonoBehaviour {

    public GameObject hook;
    public GameObject hookholder;
    private GameObject Target;
    //CamaraMouse camera_component;

    public float hookTravelSpeed;
    public float hookPlayerSpeed;
    public float maxDistance;
    public float SeekTime;
    private float currentDistance;
    private float charspeed_placeholder;
    private float gravity_placeholder;

    public static bool isFired;
    public bool isHooked;
    public GameObject hookedObj;

    private SteeringManager manager;

    private void Awake()
    {
        manager = new SteeringManager();
        charspeed_placeholder = this.GetComponent<Character>().moveSpeed;
        gravity_placeholder = this.GetComponent<Character>().GRAVITY_FALLING1;
    }

    // Use this for initialization
    void Start()
    {
        hookholder.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + 2);
        hookholder.transform.parent = transform;
        hook.transform.position = hookholder.transform.position;
        hook.transform.parent = hookholder.transform;
    }

    // Update is called once per frame
    private void Update()
    {
        if (CamaraMouse.state_Locked)
        {
            Target = CamaraMouse.lockOnTarget;
            hook.transform.forward = manager.Seek(hook.transform.position, Target.transform.position, 1.0f);
            //hook.transform.forward = manager.Pursuit(hook.transform.position, Target.transform.position, Target.transform.forward.normalized,1.0f,SeekTime);
            if (Input.GetButtonDown("Whip") || Input.GetAxis("Whip") > 0 && isFired == false)
            {
                CamaraMouse.SelectStateEnabled = false;
                isFired = true;
            }
            if (isHooked && isFired)
            {
                Character.verticalMomentum = 0f;
                hook.transform.parent = hookedObj.transform;
                transform.position = Vector3.MoveTowards(transform.position, hook.transform.position, Time.deltaTime * hookPlayerSpeed);
                float distancetoHook = Vector3.Distance(transform.position, hook.transform.position);

                if (distancetoHook < 0.5f)
                {
                    //HERE GOES EDGE GRABBING SCRIPT//
                    ReturnHook();
                }
            }
            else
            {
                //this.GetComponent<Character>().enabled = true;
                hook.transform.parent = hookholder.transform;
                //this.GetComponent<Character>().moveSpeed = charspeed_placeholder;
            }
            if (isFired && isHooked == false)
            {
                hook.transform.parent = null;
                hook.transform.position = Vector3.MoveTowards(hook.transform.position, Target.transform.position, Time.deltaTime * hookTravelSpeed);
                currentDistance = Vector3.Distance(transform.position, hook.transform.position);
                //this.GetComponent<Character>().enabled = false;
                if (currentDistance >= maxDistance)
                {
                    ReturnHook();
                }
            }
        }
    }

    public void ReturnHook()
    {
        hook.transform.rotation = hookholder.transform.rotation;
        hook.transform.position = hookholder.transform.position;
        hook.transform.parent = hookholder.transform;
        isFired = false;
        isHooked = false;
        CamaraMouse.SelectStateEnabled = true;
    }

    void Goto()
    {

    }

    void Bring()
    {

    }
}
