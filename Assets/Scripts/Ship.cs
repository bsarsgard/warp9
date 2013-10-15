using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipObject {
	public GameObject GameObject { get; set; }
	public GameObject Instance { get; set; }
	
	public ShipObject() {
	}
	
	public ShipObject(GameObject gameObject) {
		this.GameObject = gameObject;
	}
}

public class ShipDeck {
	public Dictionary<Vector3, ShipObject> Objects;
	
	public ShipDeck() {
		Objects = new Dictionary<Vector3, ShipObject>();
	}
	
	public ShipObject AddObject(Vector3 position, GameObject gameObject) {
		ShipObject cube = new ShipObject(gameObject);
		Objects.Add(position, cube);
		return cube;
	}
	
	public ShipObject RemoveObject(Vector3 position) {
		ShipObject cube = null;
		if (Objects.ContainsKey(position)) {
			cube = Objects[position];
		}
		if (cube != null) {
			Objects.Remove(position);
		}
		return cube;
	}
}

public class SerializedShip {
	public List<SerializedObject> Objects { get; set; }
}

public class SerializedObject {
	public int Deck { get; set; }
	public string GameObject { get; set; }
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }
}

public class Ship {
	public List<ShipDeck> Decks;
	
	public GameObject FloorObject { get; set; }
	public GameObject WallObject { get; set; }
	public GameObject HullObject { get; set; }
	public GameObject BridgeObject { get; set; }
	public GameObject EngineeringObject { get; set; }
	
	public Ship() {
	}
	
	public Ship(GameObject floorObject, GameObject hullObject, GameObject wallObject, GameObject bridgeObject, GameObject engineeringObject) {
		FloorObject = floorObject;
		HullObject = hullObject;
		WallObject = wallObject;
		BridgeObject = bridgeObject;
		EngineeringObject = engineeringObject;
	}
	
	public void Build() {
		Decks = new List<ShipDeck>();
		Decks.Add(new ShipDeck());
		for (int zz = 0; zz <= 4; zz++) {
			for (int xx = -3; xx <= 3; xx++) {
				Decks[0].Objects.Add(new Vector3(xx, -0.5f, zz), new ShipObject(FloorObject));
			}
		}
	}
	
	public SerializedShip Save() {
		List<SerializedObject> allObjects = new List<SerializedObject>();
		for (int ii = 0; ii < Decks.Count; ii++) {
			foreach (Vector3 key in Decks[ii].Objects.Keys) {
				ShipObject sc = Decks[ii].Objects[key];
				SerializedObject cube = new SerializedObject() { Deck = ii, GameObject = sc.GameObject.ToString(), x = key.x, y = key.y, z = key.z };
				allObjects.Add(cube);
			}
		}
		return new SerializedShip() { Objects = allObjects };
	}
	
	public void Load(SerializedShip ship) {
		Decks = new List<ShipDeck>();
		foreach (SerializedObject cube in ship.Objects) {
			if (Decks.Count <= cube.Deck) {
				for (int ii = Decks.Count; ii <= cube.Deck; ii++) {
					Decks.Add(new ShipDeck());
				}
			}
			Vector3 vector = new Vector3(cube.x, cube.y, cube.z);
			GameObject obj = null;
			switch (cube.GameObject) {
			case "Floor (UnityEngine.GameObject)":
				obj = FloorObject;
				break;
			case "Hull (UnityEngine.GameObject)":
				obj = HullObject;
				break;
			case "Wall (UnityEngine.GameObject)":
				obj = WallObject;
				break;
			case "Bridge (UnityEngine.GameObject)":
				obj = BridgeObject;
				break;
			case "Engineering (UnityEngine.GameObject)":
				obj = EngineeringObject;
				break;
			}
			Decks[cube.Deck].Objects.Add(vector, new ShipObject(obj));
		}
	}
}
