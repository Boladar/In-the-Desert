using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

public enum ItemType{AMMO, USABLE, WEAPON, ARMOUR };
public enum ArmourSlot{HEAD, BODY, FEETS, HANDS};

public class ItemDatabase : MonoBehaviour {
	
	private List<Item> database = new List<Item>();
	private JsonData itemData;

	void Start(){
		itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json"));
		ConstructItemDatabase ();
	}

	public Item GetItemByID(int id){

		for (int i = 0; i < database.Count; i++) {
			if (database [i].ID == id)
				return database[i] as Item;
		}
		Debug.LogError ("DATABASE ID IS MISSING");
		return null;
	}
	public Weapon GetWeaponByID(int id){

		for (int i = 0; i < database.Count; i++) {
			if (database [i].ID == id)
				return database[i] as Weapon;
		}
		Debug.LogError ("DATABASE ID IS MISSING");
		return null;
	}

	void ConstructItemDatabase(){
		for (int i = 0; i < itemData.Count; i++) {

			int typeINT = (int)itemData [i] ["type"];

			if ((int)itemData [i] ["id"] < 100) {
				database.Add (new Item ((int)itemData [i] ["id"], (string)itemData [i] ["title"], 
					(int)itemData [i] ["value"], (string)itemData [i] ["description"], (int)itemData [i] ["maxStackSize"],
					(string)itemData [i] ["slug"], (ItemType)typeINT));
				
			} else if ((int)itemData [i] ["id"] > 100 && (int)itemData [i] ["id"] < 200) {
				database.Add (new Weapon ((int)itemData [i] ["id"], (string)itemData [i] ["title"], 
					(int)itemData [i] ["value"], (string)itemData [i] ["description"], (int)itemData [i] ["maxStackSize"],
					(string)itemData [i] ["slug"], (ItemType)typeINT,
					(int)itemData [i] ["ammoID"], (int)itemData [i] ["range"], (int)itemData [i] ["damage"]));
				
			} else if ((int)itemData [i] ["id"] > 200 && (int)itemData [i] ["id"] < 300) {
				int slotINT = (int)itemData [i] ["slot"];

				database.Add (new Armour ((int)itemData [i] ["id"], (string)itemData [i] ["title"], 
					(int)itemData [i] ["value"], (string)itemData [i] ["description"], (int)itemData [i] ["maxStackSize"],
					(string)itemData [i] ["slug"], (ItemType)typeINT, (int)itemData[i]["defence"], (int)itemData[i]["durability"], (ArmourSlot)slotINT));
			}
					
		}
			
	}
}

public class Item
{

	public int ID { get; set;}
	public string Title{ get; set;}
	public int Value { get; set;}
	public string Description { get; set;}
	public int MaxStackSize { get; set;}
	public string Slug { get; set;}
	public Sprite Sprite { get; set;}
	public ItemType Type { get; set;}

	public Item(int id, string title, int value, string description, int maxStackSize, string slug, ItemType type){
		this.ID = id;
		this.Title = title;
		this.Value = value;
		this.Description = description;
		this.MaxStackSize = maxStackSize;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite> ("Sprites/Items/" + slug);
		this.Type = type;
	}
	public Item(){
		this.ID = -1;
	}
}

public class Weapon : Item
{
	public int AmmoID { get; set;}
	public int Range { get; set;}
	public int Damage { get; set;}

	public Weapon(int id, string title, int value, string description, int maxStackSize, string slug,ItemType type , int ammoID, int range, int damage){
		this.ID = id;
		this.Title = title;
		this.Value = value;
		this.Description = description;
		this.MaxStackSize = maxStackSize;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite> ("Sprites/Items/" + slug);
		this.Type = type;
		this.AmmoID = ammoID;
		this.Range = range;
		this.Damage = damage;
	}
}

public class Armour : Item
{
	public int Defence{ get; set;}
	public int Durability{ get; set;}
	public ArmourSlot Slot { get; set;}

	public Armour(int id, string title, int value, string description, int maxStackSize, string slug, ItemType type, int defence, int durability, ArmourSlot slot){
		this.ID = id;
		this.Title = title;
		this.Value = value;
		this.Description = description;
		this.MaxStackSize = maxStackSize;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite> ("Sprites/Items/" + slug);
		this.Type = type;
		this.Defence = defence;
		this.Durability = durability;
		this.Slot = slot;
	}

}