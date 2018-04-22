using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public float cameraMoveSpeed = 120.0f; //jak szybko porusza sie kamera
    public GameObject followObj; //co sledzi nasza kamera
    public float clampAngleH = 66.0f; //o jaki kat moze sie obrocic kamera w poziomie(glowa)
    public float clampAngleL = -40.0f; //o jaki kat moze sie obrocic kamera w poziomie(nogi)
    public float inputSensitivity = 150.0f; //jak czula jest kamera
    public float mouseX;
    public float mouseY;
    private float rotY = 0.0f;
    private float rotX = 0.0f;



    private bool isAiming;
    private Vector3 aimingOffset;
    private Vector3 defaultOffset;

    void Start ()
    {
        aimingOffset = new Vector3(.5f, 1.5f, 0f);
        defaultOffset = new Vector3(0f, 1.5f, 0f);
        SetOffset(true);

        Vector3 rot = transform.localRotation.eulerAngles;
        rotY = rot.y;
        rotX = rot.x;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
	}
	
	
	void Update ()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");



        rotY += mouseX * inputSensitivity * Time.fixedDeltaTime;
        rotX += mouseY * inputSensitivity * Time.fixedDeltaTime;


        rotX = Mathf.Clamp(rotX, clampAngleL, clampAngleH); //ograniczenie rotacji w poziomie

        Quaternion localRotation = Quaternion.Euler(rotX, rotY, 0.0f);
        transform.rotation = localRotation;
    }

    void LateUpdate()
    {
        CameraUpdater();
    }


    void CameraUpdater()
    {
        Transform target = followObj.transform;  //ustawienie obiektu ktory bedzie kamera sledzila

        //podazanie kamery za obiektem 
        float step = cameraMoveSpeed * Time.deltaTime;

       transform.position = Vector3.MoveTowards(transform.position, target.position, step);
        

    }
     public void SetOffset(bool isAiming)
    {
        if (isAiming)
        {
            followObj.transform.localPosition = aimingOffset;
        }
        else
            followObj.transform.localPosition = defaultOffset;
    }
}
