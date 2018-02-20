using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFoundationIsPlacingEnabled : MonoBehaviour {

    Vector3 leftTop;
    Vector3 rightTop;
    Vector3 leftDown;
    Vector3 rightDown;
	
	void Start ()
    {
        leftTop = transform.position;
        rightTop = transform.position;
        leftDown = transform.position;
        rightDown = transform.position;
    }
	
	
	void Update ()
    {
        //setPosition();

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

    private void OnTriggerStay(Collider other)
    {
        if (other.tag != "Ground")
        {
            BuildingFoundation.placingEnabled = false;
        }
        else
        {
            BuildingFoundation.placingEnabled = true;
        }
    }

}
