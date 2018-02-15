using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {
    public float currMaxSpeed;          //obecna predkosc maksymalna
    [SerializeField]
    public float maxSpeedSprint;        //predkosc maksymalna przy sprincie // PT ZMIANA NA PUBLIC
    [SerializeField]
    private float maxSpeedWalk;         //predkosc maksymalna przy chodzeniu
    [SerializeField]
    private float acceleration = 0;      //przyspieszenie
    [SerializeField]
    private float rotSpeed = .1f;        //predkosc obracania

    private bool isSprinting = false;

    public Vector3 previousDst;
    public float velocity;

    private void Start()
    {
        currMaxSpeed = maxSpeedWalk;
    }
    void Update () {



        Vector3 input = HandleInput();
        Vector3 dst = input * currMaxSpeed;
        dst = GetCurrSpeed(dst);
        previousDst = dst;
        transform.Translate(dst * Time.deltaTime);// ruch
        if (input.magnitude != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(input);
            transform.GetChild(0).transform.rotation =Quaternion.Slerp(transform.GetChild(0).transform.rotation, rotation,rotSpeed);//rotacja samego modelu
        }

            

    }


    private Vector3 GetInput()// pobieranie inputu
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(vertical,0, horizontal);// zamiast 'zera' moze byc cos co bedzie wykorzystywane do skakania
        input.Normalize();

        if (Input.GetKey(KeyCode.LeftShift))            //sprintowanie
            currMaxSpeed = maxSpeedSprint;
        else
            currMaxSpeed = maxSpeedWalk;
        return input;
    }


    private Vector3 HandleInput()//przerabia input tak zeby dostac kierunek ruchu dla gracza
    {
        Vector3 input = GetInput();
        Vector3 forward = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right;//kierunek na podstawie lokalnych kierunkow kamery
        forward.y = 0f;
        right.y = 0f;
        forward.Normalize();
        right.Normalize();
        Vector3 desiredMoveDirection = forward * input.x + right * input.z;


        Debug.DrawRay(transform.position, desiredMoveDirection, Color.red);
        return desiredMoveDirection;
    }

    private Vector3 GetCurrSpeed(Vector3 dst)
    {
        Vector3 velocityVector = Vector3.zero;
       dst = Vector3.SmoothDamp(previousDst, dst, ref velocityVector, 1/acceleration);      //plynne przyspieszanie/zwalnianie


        velocity = dst.magnitude;

        if (velocity < 0.0001f)
            dst = Vector3.zero;
        //Debug.Log(velocity);

        return dst;
    }

}
