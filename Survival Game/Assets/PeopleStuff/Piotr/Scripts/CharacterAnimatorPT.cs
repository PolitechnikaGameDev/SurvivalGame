using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimatorPT : MonoBehaviour {

    Animator animator; //dostep do animator controllera

	void Start ()
    {
        animator = GetComponentInChildren<Animator>(); //ustawienie animatora z dziecka Playera
	}
	
	
	void Update ()
    {
		
	}
}
