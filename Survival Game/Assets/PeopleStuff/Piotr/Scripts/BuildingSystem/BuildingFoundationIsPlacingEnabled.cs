using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFoundationIsPlacingEnabled : MonoBehaviour {

    Vector3 leftTop;
    Vector3 rightTop;
    Vector3 leftDown;
    Vector3 rightDown;
    public LayerMask layer;
    float rayDistance = 20f;
    float maxDistance = 2.5f;
    float minDistance = 0.02f;

    public bool[] placingEnabledConditions;
	
	void Start ()
    {
        leftTop = transform.position;
        rightTop = transform.position;
        leftDown = transform.position;
        rightDown = transform.position;
        placingEnabledConditions = new bool [5];
    }
	
	
	void Update ()
    {
        setPosition();
        isTouchingTheGround();
        isPlacingEnabled();
    }

    void setPosition()
    {
        leftTop = transform.position;
        rightTop = transform.position;
        leftDown = transform.position;
        rightDown = transform.position;

        leftTop.x -= 1;
        leftTop.z += 1;
        rightTop.x += 1;
        rightTop.z += 1;
        leftDown.x -= 1;
        leftDown.z -= 1;
        rightDown.x += 1;
        rightDown.z -= 1;
    }
    void isTouchingTheGround()
    {
        RaycastHit hit1;
        RaycastHit hit2;
        RaycastHit hit3;
        RaycastHit hit4;

        if(Physics.Raycast(leftTop,Vector3.down,out hit1, rayDistance, layer))
        {
            if (hit1.distance < maxDistance && hit1.distance > minDistance)
                placingEnabledConditions[0] = true;
            else
                placingEnabledConditions[0] = false;
        }

        if (Physics.Raycast(rightTop, Vector3.down, out hit2, rayDistance, layer))
        {
            if (hit2.distance < maxDistance && hit2.distance > minDistance)
                placingEnabledConditions[1] = true;
            else
                placingEnabledConditions[1] = false;
        }

        if (Physics.Raycast(leftDown, Vector3.down, out hit3, rayDistance, layer))
        {
            if (hit3.distance < maxDistance && hit3.distance > minDistance)
                placingEnabledConditions[2] = true;
            else
                placingEnabledConditions[2] = false;
        }

        if (Physics.Raycast(rightDown, Vector3.down, out hit4, rayDistance, layer))
        {
            if (hit4.distance < maxDistance && hit4.distance > minDistance)
                placingEnabledConditions[3] = true;
            else
                placingEnabledConditions[3] = false;
        }
    }

    void isPlacingEnabled()
    {
        int howManyConditionsAreTrue=0;
        for(int i = 0; i < placingEnabledConditions.Length; i++)
        {
            if (placingEnabledConditions[i])
                howManyConditionsAreTrue++;

            if (howManyConditionsAreTrue == placingEnabledConditions.Length)
                GetComponentInParent<BuildingFoundation>().placingEnabled = true;
            else
                GetComponentInParent<BuildingFoundation>().placingEnabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ground")
        {
            placingEnabledConditions[4] = false;
        }
        else
        {
            placingEnabledConditions[4] = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag != "Ground")
        {
            placingEnabledConditions[4] = true;
        }
        else
        {
            placingEnabledConditions[4] = false;
        }
    }

}
