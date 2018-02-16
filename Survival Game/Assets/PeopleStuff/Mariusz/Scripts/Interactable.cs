using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour 
{

	public float radius = 2f;
	public Transform player;

	public virtual void Interact ()
	{
		//TODO
		Debug.Log("Interacting" + transform.name);
	}

	void Update ()
	{
		float distance = Vector3.Distance (player.position, transform.position);
		if (distance <= radius)
		{
			Interact ();
		}
	}
		
	void OnDrawGizmosSelected ()
	{
		Gizmos.color = Color.yellow;
		Gizmos.DrawWireSphere (transform.position, radius);
	}
}
