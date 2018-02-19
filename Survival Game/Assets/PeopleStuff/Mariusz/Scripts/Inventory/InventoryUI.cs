﻿using UnityEngine;

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;
	public GameObject inventoryUI;

	Inventory inventory;
	InventorySlot[] slots;
	void Start () 
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		slots = itemsParent.GetComponentsInChildren<InventorySlot> ();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Inventory")) 
		{
			inventoryUI.SetActive (!inventoryUI.activeSelf);
			Cursor.visible = !Cursor.visible;
			Cursor.lockState = CursorLockMode.None;
		}
	}

	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++) 
		{
			if (i < inventory.items.Count) {
				slots [i].AddItem (inventory.items [i]);
			} else {
				slots [i].ClearSlot ();
			}
		}
		Debug.Log ("Updating UI");
	}
}
