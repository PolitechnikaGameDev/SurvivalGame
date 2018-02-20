using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFoundation : MonoBehaviour {

    bool isPlaced;
    public LayerMask layer;
    Vector3 direction;
    GameObject origin;
    float distance = 4f;

    void Start ()
    {
        isPlaced = false;
        origin = GameObject.FindGameObjectWithTag("Origin");
        direction = origin.transform.position - Camera.main.transform.position;
    }
	

	void Update ()
    {
        if (!isPlaced)
        {
            setPosition();
            placeIt();
        }
	}

    void setPosition()
    {
        direction = origin.transform.position - Camera.main.transform.position;
        direction.Normalize();
        direction *= distance;

        RaycastHit hit;
        if (Physics.Raycast(origin.transform.position, direction, out hit, distance, layer))
        {
            transform.position = hit.point;
        }
        else
        {
            transform.position = origin.transform.position + direction;
        }

    }

    void placeIt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isPlaced = true;
            BuildingManager.isBuilding = false;
        }
    }
}
