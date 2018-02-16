using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {
    [ShowOnly]
    public float currMaxSpeed;          //obecna predkosc maksymalna

    [SerializeField]
    public float maxSpeedSprint;        //predkosc maksymalna przy sprincie 
    [SerializeField]
    private float maxSpeedWalk;         //predkosc maksymalna przy chodzeniu
    [SerializeField]
    private float acceleration = 0;      //przyspieszenie
    [SerializeField]
    private float maxSlope = 1;        //maksymalne nachylenie 
    [SerializeField]
    [Range(0.001f, 0.1f)]
    private float slopeAffector;
    [SerializeField]
    private float rotSpeed = .1f;        //predkosc obracania

    [ShowOnly]
    public Vector3 KURWA;

    private Vector3 previousDst;
    public Vector3 rawInput;
    [ShowOnly]
    public Vector3 destination;
    [ShowOnly]
    public float velocity;

    private PlayerCollision playerCollision;
    private Transform playerGraphic;

    private void Start()
    {
        playerCollision = GetComponent<PlayerCollision>();
        playerGraphic = transform.GetChild(0);

        currMaxSpeed = maxSpeedWalk;
        if (playerCollision == null)
            playerCollision = gameObject.AddComponent<PlayerCollision>();
    }

    void Update () {



        Vector3 input = HandleInput();
        destination = input * currMaxSpeed;
        destination = GetCurrSpeed(destination);
        previousDst = destination;
        transform.Translate(destination * Time.deltaTime);// ruch
        if (input.magnitude != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(input);
            playerGraphic.rotation =Quaternion.Slerp(playerGraphic.rotation, rotation,rotSpeed);//rotacja samego modelu
        }

            

    }

    private Vector3 GetCurrSpeed(Vector3 dst)
    {
        Vector3 velocityVector = Vector3.zero;
  



        float slopeAngle = 90 - playerCollision.dstToNormalAngle;
 

        dst = Quaternion.AngleAxis(slopeAngle, playerGraphic.right) * dst;

        dst = Vector3.SmoothDamp(previousDst, dst, ref velocityVector, 1 / acceleration);      //plynne przyspieszanie/zwalnianie

        KURWA = AddSlopesAffection(dst,slopeAngle);
        dst += KURWA;

        Debug.DrawRay(transform.position, dst, Color.red);
        velocity = dst.magnitude;
        return dst;
    }

    private Vector3 AddSlopesAffection(Vector3 dst, float slopeAngle)
    {
        if (slopeAngle<0)
            if (Mathf.Abs(slopeAngle) >= maxSlope)
                return -dst;

        slopeAngle = slopeAngle * slopeAffector;


        return dst * slopeAngle;
    }










    private Vector3 GetInput()// pobieranie inputu
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 input = new Vector3(vertical, 0, horizontal);// zamiast 'zera' moze byc cos co bedzie wykorzystywane do skakania
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

        Debug.DrawRay(transform.position, desiredMoveDirection, Color.green);
        rawInput = desiredMoveDirection;
        return desiredMoveDirection;
    }


}
