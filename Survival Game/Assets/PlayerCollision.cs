using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollision : MonoBehaviour {
    public float playerSize = 1f;
    public float upToGroundAngle;
    public float dstToGroundAngle;


    public Vector3 dst;
    private PlayerMotor playerMotor;

    // Use this for initialization
    void Start () {
        upToGroundAngle = 0;
        dstToGroundAngle = 0;
        playerMotor = GetComponent<PlayerMotor>();
        dst = playerMotor.destination;
	}
	
	// Update is called once per frame
	void Update () {
        dst = playerMotor.destination;
        Vector3 origin =new Vector3( transform.position.x,transform.position.y,transform.position.z);
        Debug.DrawRay(origin, -Vector3.up, Color.blue);

            RaycastHit hit;
            if (Physics.Raycast(origin, -Vector3.up, out hit))
        {
            upToGroundAngle = Vector3.Angle(hit.normal, Vector3.up);

            dstToGroundAngle = Vector3.Angle(hit.normal, dst);
            if (dstToGroundAngle>90)
                Debug.Log("pod gorke");

            else
                Debug.Log("z gorki");

        }

        
	}



    private Vector3[] CalculateRaysOrigins()
    {
        Vector3 pPos = transform.position;
        Vector3[] origins = new Vector3[4];
        origins[0] = new Vector3(pPos.x - playerSize, pPos.y, pPos.z - playerSize);
        origins[1] = new Vector3(pPos.x + playerSize, pPos.y, pPos.z - playerSize);
        origins[2] = new Vector3(pPos.x - playerSize, pPos.y, pPos.z + playerSize);
        origins[3] = new Vector3(pPos.x + playerSize, pPos.y, pPos.z + playerSize);
        return origins;
    }





}
