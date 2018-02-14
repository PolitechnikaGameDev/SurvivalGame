using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorPT : MonoBehaviour {

    Animator animator; //dostep do animator controllera
    PlayerMotor playerMotor; //dostep do playerMotoru
    float LocomotionBlend = 0f;
    float maxSpeed;
    float currentSpeed;


    void Start ()
    {
        animator = GetComponent<Animator>(); //ustawienie animatora
        playerMotor = GetComponent<PlayerMotor>(); //ustawienie Player motora
        maxSpeed = playerMotor.maxSpeedSprint;
    }
	
	
	void Update ()
    {
        currentSpeed = playerMotor.velocity;
        LocomotionBlend = currentSpeed / maxSpeed;
        animator.SetFloat("LocomotionBlend", LocomotionBlend, .15f, Time.deltaTime);
    }
}
