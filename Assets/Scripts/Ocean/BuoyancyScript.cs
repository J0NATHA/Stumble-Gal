using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BuoyancyScript : MonoBehaviour
{
    public float underwaterDrag = 3f;
    public float underwaterAngularDrag = 1f;
    public float airDrag = 0f;
    public float airAngularDrag = .05f;

    private Rigidbody thisRigidBody;

    public float buoyancyForce;

    public bool hasTouchedWater;

    void Awake()
    {
        thisRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float diffY = transform.position.y;
        bool isUnderwater = diffY < 0f;
        if (isUnderwater) hasTouchedWater = true;

        if (!hasTouchedWater) return;

        if (isUnderwater)
        {
            Vector3 vector = Vector3.up * buoyancyForce * -diffY;
            thisRigidBody.AddForce(vector, ForceMode.Acceleration);
        }
        thisRigidBody.drag = isUnderwater ? underwaterDrag : airDrag;
        thisRigidBody.angularDrag = isUnderwater ? underwaterAngularDrag : airAngularDrag;
    }
}
