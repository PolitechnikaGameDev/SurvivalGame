using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

    public float minDistance = 0.3f; //minimalny dystans kamery od postaci
    public float maxDistance = 2.0f; //maksymalny dystans kamery od postaci
    public float smooth = 12f; //jak szybko kamera zmienia pozycje
    Vector3 CameraPos; // lokalizacja kamery
    //public Vector3 dollyDirAdjusted;
    public float distance; //dystans kamery od postaci


	void Awake ()
    {
        CameraPos = transform.localPosition.normalized; //ustawienie lokalizacji kamery
        distance = transform.localPosition.magnitude; //ustawienie dystansu kamery od postaci

    }
	
	
	void Update ()
    {
        Vector3 desiredCameraPos = transform.parent.TransformPoint(CameraPos * maxDistance); //odleglosc kamery od postaci
        RaycastHit hit;
        // jezeli na drodze pomiedzy postacia a kamera nastapi kolizja to albo kamera sie zblizy albo oddali
        if(Physics.Linecast(transform.parent.position,desiredCameraPos,out hit))
        {
            distance = Mathf.Clamp((hit.distance * 0.8f), minDistance, maxDistance);
        }
        else
        {
            distance = maxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, CameraPos * distance, Time.deltaTime * smooth); //ustaw kamere do poprawnej pozycji
	}
}
