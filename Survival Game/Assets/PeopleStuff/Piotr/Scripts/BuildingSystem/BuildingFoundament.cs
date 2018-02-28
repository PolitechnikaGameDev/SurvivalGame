using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFoundament : MonoBehaviour {

    [SerializeField]
    LayerMask layer;
    [SerializeField]
    GameObject finalObject;

    GameObject origin;
    Vector3 direction;
    float distance = 3.5f;
    Vector3 position;

    bool snapped;

    Vector3 rotation;

	void Start ()
    {
        origin = GameObject.FindGameObjectWithTag("Origin");
        
    }
	

	void LateUpdate ()
    {
        position = setPosition();
        rotation = setRotation();
        if (!snapped)
        {
            transform.position = position;
            transform.rotation = Quaternion.LookRotation(rotation);
        }

        placeIt();
	}

    Vector3 setPosition()
    {
        direction = origin.transform.position - Camera.main.transform.position;
        direction.Normalize();
        direction *= distance;
        Vector3 position;

        RaycastHit hit;
        if (Physics.Raycast(origin.transform.position, direction, out hit, distance, layer))
            position = hit.point;
        else
            position = origin.transform.position + direction;

        return position;
    }

    Vector3 setRotation()
    {
        Vector3 rotation = new Vector3(Camera.main.transform.forward.x, 0, Camera.main.transform.forward.z);
        return rotation;
    }

    void placeIt()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(finalObject, transform.position, transform.rotation);
        }
    }
}
