using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour {

    public GameObject[] buildingParts;
    public GameObject currentBuildingPart;
    public bool buildingON;
    public static bool isBuilding;
    public float spawnOffset = 10f;


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
                Instantiate(chooseBuildingPart(), setSpawnPosition(), Quaternion.identity);
            }
        }
	}

    bool isBuildingON()
    {
        if (Input.GetMouseButtonDown(1))
        {
            buildingON = true;
        }
        else
        {
            buildingON = false;
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

   
