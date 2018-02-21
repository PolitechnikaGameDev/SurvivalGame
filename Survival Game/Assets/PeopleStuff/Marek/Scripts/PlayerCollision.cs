using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerCollision : MonoBehaviour {
    public float playerSize = .2f;
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
            

            if (transform.position.y - hit.point.y >.1f)
                inAir = true;
            else
                inAir = false;
        }

    }





}
