using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFoundation : MonoBehaviour {

    public bool isPlaced;
    //raycasting stuff
    public LayerMask layer;
    Vector3 direction;
    GameObject origin;
    float distance = 3.5f;
    //material stuff
    public Material placingEnabledMaterial;
    public Material placingNotEnabledMaterial;
    public Material [] defaultMaterials;
    public Material [] currentMaterials;
    //collider stuff
    Collider meshCollider;
    public static bool placingEnabled;

    void Start ()
    {
        isPlaced = false;
        origin = GameObject.FindGameObjectWithTag("Origin");
        direction = origin.transform.position - Camera.main.transform.position;
        defaultMaterials = GetComponentInChildren<Renderer>().materials;
        currentMaterials = new Material [defaultMaterials.Length];
        meshCollider = GetComponentInChildren<Collider>();
    }
	

	void Update ()
    {
        if (!isPlaced)
        {
            setPosition();
            placeIt();
            changeMaterial();
            //setCollider();
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

    void changeMaterial()
    {
        if (placingEnabled)
        {
            for(int i = 0; i < defaultMaterials.Length; i++)
            {
                currentMaterials[i] = placingEnabledMaterial;
            }
        }
        else
        {
            for (int i = 0; i < defaultMaterials.Length; i++)
            {
                currentMaterials[i] = placingNotEnabledMaterial;
            }
        }

        GetComponentInChildren<Renderer>().materials = currentMaterials;

        if (isPlaced)
        {
            GetComponentInChildren<Renderer>().materials = defaultMaterials;
        }
    }

    void setCollider()
    {
        if (!isPlaced)
        {
            meshCollider.enabled = false;
        }
        else
        {
            meshCollider.enabled = true;
        }
    }

}
