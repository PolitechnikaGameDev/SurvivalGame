using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour {

    float minDistance = 0.6f; //minimalny dystans kamery od postaci
    float maxDistance = 3.0f; //maksymalny dystans kamery od postaci
    float cameraCollisionCorrectionSmooth = 24f; //jak szybko kamera zmienia pozycje podczas wykrycia kolizji
    float cameraSmooth = 3.5f; // jak szybko kamera zmienia pozycje
    float mouseScroolWhellSensitivity = 3f;
    Vector3 CameraPos; // lokalizacja kamery
    float distance; //dystans kamery od postaci
    float userDistance; //dystans ustawiony przez uzytkownika
    public LayerMask layer;


	void Awake ()
    {
        CameraPos = transform.localPosition.normalized; //ustawienie lokalizacji kamery
        distance = transform.localPosition.magnitude; //ustawienie dystansu kamery od postaci
        userDistance = distance;

    }
	
	
	void Update ()
    {
        ZoomCamera();
        SetCameraPosition();
    }

    void ZoomCamera()
    {
        userDistance -= Input.GetAxis("Mouse ScrollWheel") * mouseScroolWhellSensitivity;
        userDistance = Mathf.Clamp(userDistance, minDistance, maxDistance);
    }

    void SetCameraPosition()
    {
        // jezeli na drodze pomiedzy postacia a kamera nastapi kolizja to zblizy kamere
        Vector3 desiredCameraPos = transform.parent.TransformPoint(CameraPos * userDistance); //odleglosc kamery od postaci
        RaycastHit hit;
        if (Physics.Linecast(transform.parent.position, desiredCameraPos, out hit,layer))
        {
            distance = Mathf.Clamp((hit.distance * 0.8f), minDistance, maxDistance);
            transform.localPosition = Vector3.Lerp(transform.localPosition, CameraPos * distance, Time.deltaTime * cameraCollisionCorrectionSmooth); //ustaw kamere do poprawnej pozycji
        }
        else
        {
            distance = userDistance;
            transform.localPosition = Vector3.Lerp(transform.localPosition, CameraPos * distance, Time.deltaTime * cameraSmooth); //ustaw kamere do poprawnej pozycji
        }
    }
}
