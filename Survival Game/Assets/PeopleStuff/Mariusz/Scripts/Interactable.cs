using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour 
{
	public virtual void Interact ()
	{
		//TODO
		Debug.Log("Interacting" + transform.name);
	}

	void Update ()
	{
		
	}
		

}
