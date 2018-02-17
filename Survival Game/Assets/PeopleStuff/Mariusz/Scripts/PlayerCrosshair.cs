using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrosshair : MonoBehaviour {

	public Camera cam;
	public LayerMask mask;
	void Update () 
	{
		
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, 100, mask.value))
		{
			
			if (Input.GetButtonDown ("Interact")) 
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
