﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshair : MonoBehaviour {

	public float interactionDist = 2.5f;
	public Camera cam;
	public LayerMask mask;
	void Update () 
	{
		
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, 100f, mask.value))
		{
			Debug.Log ("Hit" + hit.transform.name);
			Interactable interactable = hit.collider.GetComponent<Interactable> ();
			float distance = Vector3.Distance (interactable.transform.position, transform.position);
			if (Input.GetButtonDown ("Interact") && distance <= interactionDist) 
			{
				if (interactable != null) 
				{
					interactable.Interact ();
				}
			}
		}
	}


}
