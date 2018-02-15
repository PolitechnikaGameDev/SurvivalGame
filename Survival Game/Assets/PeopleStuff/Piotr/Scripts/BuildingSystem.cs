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
    Transform activeBuidlingPart;



    void Start ()
    {
        direction = origin.transform.position - Camera.main.transform.position;
        enableInstantiate = true;
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
            Debug.Log(hit.transform.name);
                if (enableInstantiate)
                {
                    buildingPosition = hit.point;
                    activeBuidlingPart = Instantiate(buildingBlock, buildingPosition, Quaternion.identity);
                    enableInstantiate = false;
                }
                activeBuidlingPart.position = hit.point;
            }
        }


        Debug.DrawRay(origin.transform.position, direction, Color.red, Time.deltaTime*2f);
    }
}
