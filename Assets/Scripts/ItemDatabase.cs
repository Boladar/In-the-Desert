﻿using UnityEngine;
using System.Collections;
using LitJson;
using System.Collections.Generic;
using System.IO;

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
				return database[i];
		}
		return null;
	}

	void ConstructItemDatabase(){
		for (int i = 0; i < itemData.Count; i++) {
			database.Add (new Item ( (int)itemData[i]["id"], (string)itemData[i]["title"], 
				(int)itemData[i]["value"],(string)itemData[i]["description"],(int)itemData[i]["maxStackSize"], (string)itemData[i]["slug"]));
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

	public Item(int id, string title, int value, string description, int maxStackSize, string slug){
		this.ID = id;
		this.Title = title;
		this.Value = value;
		this.Description = description;
		this.MaxStackSize = maxStackSize;
		this.Slug = slug;
		this.Sprite = Resources.Load<Sprite> ("Sprites/Items/" + slug);
	}
	public Item(){
		this.ID = -1;
	}
}