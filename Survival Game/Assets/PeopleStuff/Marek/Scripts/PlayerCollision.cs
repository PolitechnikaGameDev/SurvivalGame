using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCollision : MonoBehaviour {
#if UNITY_EDITOR
    public float playerSize = .2f;          //zmieniac to w #else ponizej
    [ShowOnly]
    public float upToNormalAngle;
    [ShowOnly]
    public float dstToNormalAngle;
    [ShowOnly]
    public bool inAir;
    [HideInInspector]
    public Vector3 rotateAxis;

    public float rayOriginOffsetY = 0.4f;
    private Vector3 dst;
    private PlayerMotor playerMotor;
#else
        public float playerSize = .2f;

    public float upToNormalAngle;

    public float dstToNormalAngle;

    public bool inAir;

    public Vector3 rotateAxis;

    public float rayOriginOffsetY = 0.4f;
    private Vector3 dst;
    private PlayerMotor playerMotor;

#endif


    // Use this for initialization
    void Start () {
        upToNormalAngle = 0;
        dstToNormalAngle = 0;
        playerMotor = GetComponent<PlayerMotor>();
        dst = playerMotor.destination;
	}

    // Update is called once per frame
    void Update() {
        dst = playerMotor.rawInput;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y+ rayOriginOffsetY, transform.position.z);
        origin = origin + dst*0.3f;
        Debug.DrawRay(origin, -Vector3.up, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit))
        {
            upToNormalAngle = Vector3.Angle(hit.normal, Vector3.up);

            dstToNormalAngle = Vector3.Angle(hit.normal,dst);

            if(dstToNormalAngle < 90)
            rotateAxis = Vector3.Cross(hit.normal, -Vector3.up);
            else
                rotateAxis = Vector3.Cross(hit.normal, Vector3.up);
            


        }

    }
    private void OnCollisionEnter(Collision other)
    {
        foreach(ContactPoint point in other.contacts)
        {
            if (point.thisCollider.GetType() == typeof(SphereCollider) && point.point.y < transform.position.y)
                inAir = false;
            
        }
    }
    private void OnCollisionExit(Collision other)
    {
        inAir = true;
    }





}
