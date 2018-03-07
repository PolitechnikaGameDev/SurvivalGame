using UnityEngine;

public class InventoryUI : MonoBehaviour {

	public Transform itemsParent;
	public GameObject inventoryUI;
    public GameObject crosshair;

	Inventory inventory;
	InventorySlot[] slots;

	void Start () 
	{
		inventory = Inventory.instance;
		inventory.onItemChangedCallback += UpdateUI;
		slots = itemsParent.GetComponentsInChildren<InventorySlot> ();

        crosshair = transform.Find("Crosshair").gameObject;
    }
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetButtonDown ("Inventory")) 
		{
			inventoryUI.SetActive (!inventoryUI.activeSelf);
			if (inventoryUI.activeInHierarchy) 
			{
				Cursor.visible = true;
				Cursor.lockState = CursorLockMode.None;
			}
			if (!inventoryUI.activeInHierarchy) 
			{
				Cursor.visible = false;
				Cursor.lockState = CursorLockMode.Locked;
			}

		}

	}

	void UpdateUI()
	{
		for (int i = 0; i < slots.Length; i++) 
		{
			if (i < inventory.items.Count) 
			{
				slots [i].AddItem (inventory.items [i]);
			} 
			else 
			{
				slots [i].ClearSlot ();
			}
		}
		Debug.Log ("Updating UI");
	}
    public void UpdateCoursor(bool isAiming)
    {
        crosshair.SetActive(isAiming);
    }
}
