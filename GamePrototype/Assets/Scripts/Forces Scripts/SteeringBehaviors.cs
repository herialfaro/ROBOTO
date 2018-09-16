using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteeringManager
{
    public Vector3 Seek(Vector3 pos, Vector3 obj, float m)
    {
        Vector3 dir = obj - pos;
        dir.Normalize();
        return dir * m;
    }

    public Vector3 Pursuit(Vector3 pos, Vector3 obj, Vector3 vel, float m, float t)
    {
        Vector3 futurePosition = obj + vel * t;
        return Seek(pos,futurePosition,m);
    }

    public Vector3 Arrive(Vector3 pos, Vector3 obj, float m, float r)
    {
        Vector3 dir = pos - obj;
        float dist = dir.magnitude;
        if (dist < r)
        {
            dir.Normalize();
            dir = dir * (dist / r);
        }
        else
        {
            dir.Normalize();
        }
        return dir * m * 4;
    }

    public Vector3 Flee(Vector3 pos, Vector3 obj, float m, float r)
    {
        Vector3 dir = pos - obj;
        if (dir.magnitude < r)
        {
            dir.Normalize();
        }
        else
        {
            dir = dir * 0;
        }
        return dir * m;
    }

    public Vector3 Wander(Vector3 pos, Vector3 obj, float m, float a, float r, float da)
    {
        Vector3 circleCenter = new Vector3(0.0f, 0.0f, 0.0f);
        circleCenter = obj;
        circleCenter.Normalize();
        circleCenter *= Vector3.Distance(pos, obj);

        Vector3 displacement = new Vector3(0.0f, 0.0f, -1.0f);
        displacement *= r;

        float range = Random.Range(0.0f, 1.0f);
        a += (range * Mathf.PI * da) - (Mathf.PI * da * .5f);

        displacement = SetAngle(displacement, a);

        Vector3 wanderForce = circleCenter + displacement;
        return wanderForce;
    }

    public Vector3 SetAngle(Vector3 dis, float angle)
    {
        Vector3 vec = new Vector3(0.0f, 0.0f, 0.0f);
        float len = dis.magnitude;
        vec.x = Mathf.Cos(angle) * len;
        vec.z = Mathf.Sin(angle) * len;
        return vec;
    }

    //public float DistbetweenVectors(Vector3 a, Vector3 b) //ON GROUND
    //{
    //    return (float)System.Math.Sqrt((a.x - b.x) * (a.x - b.x) + (a.z - b.z) * (a.z - b.z));
    //}

    //public bool CircleIntersection(Vector3 ahead, Vector3 ahead2, Vector3 obj, float r)
    //{
    //    return DistbetweenVectors(obj, ahead) <= r || DistbetweenVectors(obj, ahead2) <= r;
    //}

    public Vector3 Truncate(Vector3 a, float m)
    {
        double i = 0;
        i = m / a.magnitude;
        i = i < 1.0 ? i : 1.0;
        Vector3 scale = new Vector3();
        scale.Set((float)i, (float)i, (float)i);
        a.Scale(scale);
        return a;
    }
};
