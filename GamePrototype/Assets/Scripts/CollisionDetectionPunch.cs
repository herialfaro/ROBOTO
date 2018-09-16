using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetectionPunch : MonoBehaviour {

    public int type;
    public float radius;
    private List<GameObject> targets_inradius;
    private GameObject[] lockable_objects;

    private void Awake()
    {
        targets_inradius = new List<GameObject>();
    }

    private void Start()
    {
        lockable_objects = GameObject.FindGameObjectsWithTag("Lockable");
    }

    private void Update()
    {
        if (AnimacionesRoboto.punch_enabled)
        {
            for (int i = 0; i < lockable_objects.Length; i++)
            {
                if (Vector3.Distance(lockable_objects[i].transform.position, this.gameObject.transform.position) <= radius && lockable_objects[i].gameObject.layer == type)
                {
                    CamaraMouse.Lookforobjects = true;
                    targets_inradius.Add(lockable_objects[i]);
                }
            }
            Debug.Log(targets_inradius.Count);
            PunchAllObjects();
            targets_inradius.Clear();
        }
    }

    private void PunchAllObjects()
    {
        for (int i = targets_inradius.Count - 1; i >= 0; i--)
        {
            CamaraMouse.state_Locked = false;
            Debug.Log(targets_inradius[i].name);
            targets_inradius[i].gameObject.SetActive(false);
            targets_inradius[i].gameObject.tag = "Untagged";
            targets_inradius[i].gameObject.layer = 0;
            targets_inradius.RemoveAt(i);
        }
    }
}
