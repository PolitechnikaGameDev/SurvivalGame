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
        public bool inAir;


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
            inAir = pc.inAir;

        }

    };

#if UNITY_EDITOR


    [ShowOnly]
    public float currMaxSpeed;          //obecna predkosc maksymalna

    [SerializeField]
    public float maxSpeedSprint = 6;        //predkosc maksymalna przy sprincie 
    [SerializeField]
    private float maxSpeedWalk = 3;         //predkosc maksymalna przy chodzeniu
    [SerializeField]
    private float acceleration = 20;      //przyspieszenie
    [SerializeField]
    private float jumpForce = 300;      //sila skoku
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
    private CameraFollow cameraFollow;
    private Rigidbody rigidBody;
    private Vector3 previousDst;
    private Transform mainCameraT;
    private InventoryUI inventoryUI;

    [HideInInspector]
    public bool isAiming;
#else

    public float currMaxSpeed;          //obecna predkosc maksymalna


    public float maxSpeedSprint = 6;        //predkosc maksymalna przy sprincie 

    private float maxSpeedWalk = 3;         //predkosc maksymalna przy chodzeniu

    private float acceleration = 20;      //przyspieszenie

    private float jumpForce = 300;      //sila skoku

    private float maxSlope = 60;        //maksymalne nachylenie 


    private float slopeAffector =  0.01f;

    private float rotSpeed = .1f;        //predkosc obracania


    public Vector3 destination;

    public float velocity;


    public Vector3 rawInput;

    private Transform playerGraphic;
    private CollisionData collisionData;
    private Rigidbody rigidBody;
    private Vector3 previousDst;
#endif

    private void Start()
    {
        mainCameraT = Camera.main.transform;
        if (GetComponent<PlayerCollision>() == null)
            gameObject.AddComponent<PlayerCollision>();
        

            collisionData = new CollisionData(GetComponent<PlayerCollision>());

        rigidBody = GetComponent<Rigidbody>();

        cameraFollow = GameObject.Find("CameraBase").GetComponent<CameraFollow>();
        isAiming = false;
        cameraFollow.SetOffset(isAiming);
        inventoryUI = GameObject.Find("Canvas").GetComponent<InventoryUI>();
        inventoryUI.UpdateCoursor(isAiming);

        playerGraphic = transform.GetChild(0);

        currMaxSpeed = maxSpeedWalk;

    }
    void Update () {

		//if (EventSystem.current.IsPointerOverGameObject ())
			//return;

        Vector3 input = HandleInput();
        collisionData.Update();

        if (rawInput.magnitude != 0)
        {
            Quaternion rotation;

            if (isAiming)
                rotation = Quaternion.LookRotation(new Vector3(mainCameraT.forward.x, 0, mainCameraT.forward.z));
            else
                rotation = Quaternion.LookRotation(rawInput);


            playerGraphic.rotation = Quaternion.Slerp(playerGraphic.rotation, rotation, rotSpeed);//rotacja samego modelu
        }

    }
    private void FixedUpdate()
    {
        destination = GetCurrSpeed(rawInput);
        rigidBody.MovePosition(transform.position + destination * Time.deltaTime);

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

        if (Input.GetKeyDown(KeyCode.Space) && !collisionData.inAir)
        {
            rigidBody.AddForce(Vector3.up * jumpForce);
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 0.5f, transform.localScale.z);
        }
        else if (Input.GetKeyUp(KeyCode.C))
        {
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y * 2f, transform.localScale.z);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            isAiming = !isAiming;
            cameraFollow.SetOffset(isAiming);
            inventoryUI.UpdateCoursor(isAiming);
        }
            return input;
    }
    

    private Vector3 HandleInput()//przerabia input tak zeby dostac kierunek ruchu dla gracza
    {
        Vector3 input = GetInput();
        Vector3 forward = mainCameraT.forward;
        Vector3 right = mainCameraT.right;//kierunek na podstawie lokalnych kierunkow kamery
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
