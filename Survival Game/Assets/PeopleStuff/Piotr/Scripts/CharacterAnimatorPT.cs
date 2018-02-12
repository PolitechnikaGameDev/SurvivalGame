using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorPT : MonoBehaviour {

    Animator animator; //dostep do animator controllera

	void Start ()
    {
        animator = GetComponent<Animator>(); //ustawienie animatora
	}
	
	
	void Update ()
    {
        float LocomotionBlend = 0.5f;

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        animator.SetFloat("LocomotionBlend", LocomotionBlend, .1f, Time.deltaTime);


    }
}
