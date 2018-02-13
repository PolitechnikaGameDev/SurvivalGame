using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorMT : MonoBehaviour {

    Animator animator;
    PlayerMotor playerMotor;
    float locomotionBlend = 0;
    float maxSpeed;

    void Start()
    {
        animator = GetComponent<Animator>(); //ustawienie animatora
        playerMotor = GetComponent<PlayerMotor>(); //ustawienie Player motora


    }

    void Update () {
        maxSpeed = playerMotor.currMaxSpeed;
        locomotionBlend = map(playerMotor.velocity, 1, maxSpeed);

        animator.SetFloat("LocomotionBlend", locomotionBlend, .1f, Time.deltaTime);

    }

    float map(float s, float gornyA,float gornyB)
    {
        return s * (gornyB / gornyA);
    }
}
