using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshair : MonoBehaviour {

	public Camera cam;

	void Update () 
	{
		if (Input.GetButtonDown ("Interact")) 
		{
			RaycastHit hit;
			if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit)) 
			{
				Debug.Log ("Hit" + hit.transform.name);
				Interactable interactable = hit.collider.GetComponent<Interactable> ();
				if (interactable != null) 
				{
					interactable.clicked = true;
				}
			}
		}
	}


}
