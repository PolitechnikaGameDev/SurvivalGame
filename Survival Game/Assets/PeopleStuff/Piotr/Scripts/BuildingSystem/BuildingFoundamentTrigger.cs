using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingFoundamentTrigger : MonoBehaviour {

    Vector3 snappedPosition;
    bool snapped;
    Vector3 snappedRotation;
    Vector3 offset;

    void Start()
    {
        snapped = transform.GetComponentInParent<BuildingFoundament>().snapped;
        //snappedPosition = transform.GetComponentInParent<BuildingFoundament>().snappedPosition;
        //snappedRotation = transform.GetComponentInParent<BuildingFoundament>().snappedRotation;
    }

    void OnTriggerEnter(Collider other)
    {
        if (!transform.GetComponentInParent<BuildingFoundament>().snapped)
        {
            if (other.tag == "Foundation")
            {
                if (this.transform.name == "SnapN")
                {
                    snappedPosition = other.transform.localPosition;
                    snappedPosition.z -= 2.5f;
                    Debug.Log(other.transform.position);
                }
                else if (this.transform.name == "SnapE")
                {
                    snappedPosition = other.transform.position;
                    //snappedPosition.x += 2.5f;
                }
                else if (this.transform.name == "SnapS")
                {
                    snappedPosition = other.transform.position;
                    snappedPosition.z += 2.5f;
                }
                else if (this.transform.name == "SnapW")
                {
                    snappedPosition = other.transform.position;
                    //snappedPosition.x -= 2.5f;
                }

                //snappedPosition.y += 2.5f;
                //transform.GetComponentInParent<BuildingFoundament>().snappedRotation = new Vector3(other.transform.forward.x, other.transform.forward.y, other.transform.forward.z);
                transform.GetComponentInParent<BuildingFoundament>().snappedPosition = snappedPosition;
                transform.GetComponentInParent<BuildingFoundament>().snapped = true;
            }
        }


    }
}
