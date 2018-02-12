﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerMotor : MonoBehaviour {
    public float speed = 0;

	void Start () {


    }
	
	void Update () {
        Vector3 input = HandleInput();
        if (input.magnitude != 0)
        {
            transform.Translate(input * speed * Time.deltaTime);// ruch

            Quaternion rotation = Quaternion.LookRotation(input);
            transform.GetChild(0).transform.rotation = rotation;//rotacja samego modelu
        }


    }

    private Vector3 GetInput()// pobieranie inputu
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 input = new Vector3(vertical,0, horizontal);// zamiast 'zera' moze byc cos co bedzie wykorzystywane do skakania
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

}