using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour 
{

	public float radius = 2f;

	bool isFocus = false;
	bool hasInteracted = false;
	Transform player;

	public virtual void Interact ()
	{
		//TODO
		Debug.Log("Interacting" + transform.name);
	}

	void Update ()
	{
		if (isFocus && !hasInteracted) 
		{
			float distance = Vector3.Distance (player.position, transform.position);
			if (distance <= radius) 
			{
				Interact ();
				hasInteracted = true;
			}
		}
	}

	public void OnFocused (Transform playerTransform)
	{
		isFocus = true;
		player = playerTransform;
		hasInteracted = false;
	}

	public void OnDefocused ()
	{
		isFocus = false;
		hasInteracted = false;
	}
		
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}
