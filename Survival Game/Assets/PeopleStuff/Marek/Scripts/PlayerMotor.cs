using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;




public class PlayerMotor : MonoBehaviour {
    private class CollisionData
    {
       
        public float upToNormalAngle;
        public float dstToNormalAngle;
        public Vector3 rotateAxis;
        public PlayerCollision pc;
        public float slopeAngle;

        public CollisionData(PlayerCollision _pc)
        {
            pc = _pc;
            Update();
        }
        public void Update()
        {
            upToNormalAngle = pc.upToNormalAngle;
            dstToNormalAngle = pc.dstToNormalAngle;
            rotateAxis = pc.rotateAxis;
            slopeAngle = 90 - pc.dstToNormalAngle;
        }

    };

    [ShowOnly]
    public float currMaxSpeed;          //obecna predkosc maksymalna

    [SerializeField]
    public float maxSpeedSprint = 6;        //predkosc maksymalna przy sprincie 
    [SerializeField]
    private float maxSpeedWalk = 3;         //predkosc maksymalna przy chodzeniu
    [SerializeField]
    private float acceleration = 20;      //przyspieszenie
    [SerializeField]
    private float maxSlope = 60;        //maksymalne nachylenie 
    [SerializeField]
    [Range(0.001f, 0.2f)]
    private float slopeAffector =  0.01f;
    [SerializeField]
    private float rotSpeed = .1f;        //predkosc obracania

    [ShowOnly]
    public Vector3 destination;
    [ShowOnly]
    public float velocity;




    [HideInInspector]
    public Vector3 rawInput;
    private Transform playerGraphic;
    private CollisionData collisionData;
    private Vector3 previousDst;
    private void Start()
    {
        if (GetComponent<PlayerCollision>() == null)
            gameObject.AddComponent<PlayerCollision>();

        collisionData = new CollisionData(GetComponent<PlayerCollision>());

        playerGraphic = transform.GetChild(0);

        currMaxSpeed = maxSpeedWalk;
    }
    void Update () {

		if (EventSystem.current.IsPointerOverGameObject ())
			return;

        Vector3 input = HandleInput();
        collisionData.Update();
        destination = GetCurrSpeed(input);


        transform.Translate(destination * Time.deltaTime);// ruch
        if (input.magnitude != 0)
        {
            Quaternion rotation = Quaternion.LookRotation(input);
            playerGraphic.rotation =Quaternion.Slerp(playerGraphic.rotation, rotation,rotSpeed);//rotacja samego modelu
        }

            

    }


    private Vector3 GetCurrSpeed(Vector3 input)
    {


        Vector3 dst = input * currMaxSpeed;

        dst = RotateDestination(dst);

        dst += AddSlopesAffection(dst,collisionData.slopeAngle);

        dst = SmoothDestination(dst);

        velocity = destination.magnitude;

        previousDst = destination;

        Debug.DrawRay(transform.position, dst, Color.red);
        return dst;
    }

    private Vector3 RotateDestination(Vector3 dst)
    {
        dst = Quaternion.AngleAxis(collisionData.slopeAngle, collisionData.rotateAxis) * dst;
        Debug.DrawRay(playerGraphic.position, collisionData.rotateAxis);
        return dst;
    }

    private Vector3 SmoothDestination(Vector3 dst)
    {

        Vector3 velocityVector = Vector3.zero;
        dst = Vector3.SmoothDamp(previousDst, dst, ref velocityVector, 1 / acceleration);      //plynne przyspieszanie/zwalnianie
        return dst;
    }

    private Vector3 AddSlopesAffection(Vector3 dst, float slopeAngle)
    {
        if (slopeAngle<0)
            if (collisionData.upToNormalAngle >= maxSlope)
                return -dst;

        slopeAngle *= slopeAffector;


        return dst * slopeAngle *slopeAffector;
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
