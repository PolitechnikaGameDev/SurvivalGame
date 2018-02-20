using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject[] buildingParts;
    public GameObject currentBuildingPart;
    public bool buildingON;
    public static bool isBuilding;
    public float spawnOffset = 10f;
    GameObject instantiatedObject;



    void Start ()
    {
        currentBuildingPart = buildingParts[0];
	}
	

	void Update ()
    {
        if (isBuildingON())
        {
            if (!isBuilding)
            {
                isBuilding = true;
                instantiatedObject = Instantiate(chooseBuildingPart(), setSpawnPosition(), Quaternion.identity);
            }
        }
        else
        {
            Destroy(instantiatedObject);
            isBuilding = false;
        }
    }

    bool isBuildingON()
    {
        if (Input.GetKeyDown("0"))
        {
            buildingON = !buildingON;
        }
        return buildingON;
    }

    //wybieranie jaki blok chcemy wlasnie postawic
    GameObject chooseBuildingPart()
    {
        currentBuildingPart = buildingParts[0];
        return currentBuildingPart;
    }

    //ustawienie spawnu bloczku na poczatek
    Vector3 setSpawnPosition()
    {
        Vector3 spawnPosition = transform.position;
        spawnPosition.y -= spawnOffset;
        return spawnPosition;
    }
}

   
