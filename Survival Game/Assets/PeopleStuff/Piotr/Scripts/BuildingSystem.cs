using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingSystem : MonoBehaviour {

    public LayerMask layer;
    public Transform buildingBlock;
    public GameObject origin;
    Vector3 direction;
    Vector3 buildingPosition;
    float distance=5f;
    bool enableInstantiate;
    bool buildingOn;
    Transform activeBuidlingPart;



    void Start ()
    {
        direction = origin.transform.position - Camera.main.transform.position;
        enableInstantiate = true;
        buildingOn = true;
	}
	
	void Update ()
    {
        direction = origin.transform.position - Camera.main.transform.position;
        direction.Normalize();
        direction *= distance;

        RaycastHit hit;
        if(Physics.Raycast(origin.transform.position,direction,out hit, distance,layer))
        {
            if (hit.transform != this.transform)
            {
                if (enableInstantiate)
                {
                    buildingPosition = hit.point;
                    activeBuidlingPart = Instantiate(buildingBlock, buildingPosition, Quaternion.identity);
                    enableInstantiate = false;
                }
                activeBuidlingPart.position = hit.point;
            }
        }


        //Debug.DrawRay(origin.transform.position, direction, Color.red, Time.deltaTime*2f);

        /*if (buildingOn)
        {
            buildingPosition = setPosition();
            if (enableInstantiate)
            {
                activeBuidlingPart = Instantiate(buildingBlock, buildingPosition, Quaternion.identity);
                enableInstantiate = false;
            }
            activeBuidlingPart.position =buildingPosition;
        }*/
    }

    Vector3 setPosition()
    {
        direction = origin.transform.position - Camera.main.transform.position;
        direction.Normalize();
        direction *= distance;
        Vector3 position;

        RaycastHit hit;
        Physics.Raycast(origin.transform.position, direction, out hit, distance, layer);
        if (hit.transform != this.transform)
            position = hit.point;
        else
            position = origin.transform.position + direction;

        return position;
    }
}
