using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFoundationSnapping : MonoBehaviour
{

    Vector3 snapPosition;
    void Start()
    {

    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag=="Foundation")
        {
            Debug.Log("cos");
        }
    }

}