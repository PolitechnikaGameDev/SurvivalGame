using UnityEngine;

[CreateAssetMenu(fileName = "New item",menuName = "Inventory/Item")]
public class Item : ScriptableObject {

	new public string name = "New Item";
	public Sprite icon = null;
	public bool isDefaultItem = false;
}
