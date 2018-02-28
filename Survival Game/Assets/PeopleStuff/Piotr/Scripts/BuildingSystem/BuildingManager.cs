using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    [SerializeField]
    GameObject[] buildingParts;
    GameObject currentPart;
    GameObject instantiatedObject;

    bool buildingON;
    bool isBuilding;
    bool currentPartChanged;

    Vector3 spawnPosition;

	void Start ()
    {
        currentPart = buildingParts[0];
	}
	

	void Update ()
    {
        if (isBuildingOn())
        {
            chooseBuildingPart();

            if (!isBuilding && !currentPartChanged)
            {
                isBuilding = true;
                instantiatedObject = Instantiate(currentPart, setSpawnPosition(), Quaternion.identity);
            }
            if(currentPartChanged)
            {
                Destroy(instantiatedObject);
                isBuilding = true;
                instantiatedObject = Instantiate(currentPart, setSpawnPosition(), Quaternion.identity);
            }
        }
        else
        {
            Destroy(instantiatedObject);
            isBuilding = false;
            currentPart = buildingParts[0];
        }
	}

    bool isBuildingOn()
    {
        if (Input.GetKeyDown("0"))
            buildingON = !buildingON;

        return buildingON;
    }

    void chooseBuildingPart()
    {
        GameObject choosedPart = currentPart;

        if (Input.GetKeyDown("1"))
            choosedPart = buildingParts[0];
        else if (Input.GetKeyDown("2"))
            choosedPart = buildingParts[1];
        else if (Input.GetKeyDown("3"))
            choosedPart = buildingParts[2];
        else if (Input.GetKeyDown("4"))
            choosedPart = buildingParts[3];
        else if (Input.GetKeyDown("5"))
            choosedPart = buildingParts[4];

        if(choosedPart!=currentPart)
            currentPartChanged=true;

        if (currentPartChanged)
            currentPart = choosedPart;

    }

    Vector3 setSpawnPosition()
    {
        spawnPosition = transform.position;
        spawnPosition.y -= 20f;
        return spawnPosition;
    }
}
