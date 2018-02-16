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
    /*
    [ShowOnly]
    public string aaaaaaa = "Przod";
    [ShowOnly]
    public float zielony, blekitny;
    [ShowOnly]
    public string bbbbbbb = "Tyl";
    [ShowOnly]
    public float  czerwony, niebieski;

    */

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
	void Update () {
        dst = playerMotor.rawInput;
        Vector3 origin = new Vector3(transform.position.x, transform.position.y+.2f, transform.position.z);
        Debug.DrawRay(origin, -Vector3.up, Color.blue);

        RaycastHit hit;
        if (Physics.Raycast(origin, -Vector3.up, out hit))
        {
            upToNormalAngle = Vector3.Angle(hit.normal, Vector3.up);

            dstToNormalAngle = Vector3.Angle(hit.normal,dst);
        }

    }


/*
    private Vector3[] CalculateRaysOrigins()
    {
        Vector3 pPos = transform.position;
        Vector3[] origins = new Vector3[4];
        origins[0] = new Vector3(pPos.x - playerSize, pPos.y+.5f, pPos.z - playerSize);
        Debug.DrawRay(origins[0], -Vector3.up, Color.blue);
        origins[1] = new Vector3(pPos.x + playerSize, pPos.y+.5f, pPos.z - playerSize);
        Debug.DrawRay(origins[1], -Vector3.up, Color.red);
        origins[2] = new Vector3(pPos.x - playerSize, pPos.y+.5f, pPos.z + playerSize);
        Debug.DrawRay(origins[2], -Vector3.up, Color.cyan);
        origins[3] = new Vector3(pPos.x + playerSize, pPos.y+.5f, pPos.z + playerSize);
        Debug.DrawRay(origins[3], -Vector3.up, Color.green);


        RaycastHit hit;
        if (Physics.Raycast(origins[0], -Vector3.up, out hit))
        {
            niebieski = hit.point.y;
        }
        if (Physics.Raycast(origins[1], -Vector3.up, out hit))
        {
            czerwony = hit.point.y;
        }
        if (Physics.Raycast(origins[2], -Vector3.up, out hit))
        {
            blekitny = hit.point.y;
        }
        if (Physics.Raycast(origins[3], -Vector3.up, out hit))
        {
            zielony= hit.point.y;
        }


        Vector3 high = new Vector3(), low = new Vector3();

        if (origins[0].y > origins[1].y)
        {
            high.x = origins[0].x;
            low.x = origins[1].x;
        }
        else
            high.x = origins[1].x;
            low.x = origins[0].x;



        return origins;
    }

    */




}
