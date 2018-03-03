using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float CameraMoveSpeed = 120.0f; //jak szybko porusza sie kamera
    public GameObject CameraFollowObj; //co sledzi nasza kamera
    public float clampAngleH = 66.0f; //o jaki kat moze sie obrocic kamera w poziomie(glowa)
    public float clampAngleL = -40.0f; //o jaki kat moze sie obrocic kamera w poziomie(nogi)
    public float inputSensitivity = 150.0f; //jak czula jest kamera
    public float mouseX;
    public float mouseY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;

    private Rigidbody rigidBody;

    void Start ()
    {
        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        if (!(rigidBody = GetComponent<Rigidbody>()))
        {
            rigidBody = gameObject.AddComponent<Rigidbody>();
            rigidBody.useGravity = false;
        }
	}
	
	
	void Update ()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

	}

    void FixedUpdate()
    {
        CameraUpdater();


        rotY += mouseX * inputSensitivity * Time.fixedDeltaTime;
        rotX += mouseY * inputSensitivity * Time.fixedDeltaTime;

        rotX = Mathf.Clamp(rotX, clampAngleL, clampAngleH); //ograniczenie rotacji w poziomie

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        rigidBody.MoveRotation(localRotation);

    }


    void CameraUpdater()
    {
        Transform target = CameraFollowObj.transform;  //ustawienie obiektu ktory bedzie kamera sledzila

        //podazanie kamery za obiektem 
        float step = CameraMoveSpeed * Time.fixedDeltaTime;

        rigidBody.MovePosition(Vector3.MoveTowards(transform.position, target.position, step));

    }
}
