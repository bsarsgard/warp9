using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipObject {
	public GameObject GameObject { get; set; }
	public GameObject Instance { get; set; }
	public Vector3 Position { get; set; }
	
	public ShipObject() {
	}
	
	public ShipObject(GameObject gameObject, Vector3 position) {
		this.GameObject = gameObject;
		this.Position = position;
	} 
}

public class ShipFeature: ShipObject {
	public ShipFeature ParentFeature { get; set; }
	public Vector3 EulerAngles { get; set; }
}

public class ShipCell {
	public ShipObject Floor { get; set; }
	public ShipObject Feature { get; set; }
}

public class ShipDeck {
	public Dictionary<Vector2, ShipCell> Cells;
	
	public ShipDeck() {
		Cells = new Dictionary<Vector2, ShipCell>();
	}
	
	public ShipObject RemoveFloor(Vector2 position) {
		ShipObject obj = null;
		if (Cells.ContainsKey(position)) {
			obj = Cells[position].Floor;
			Cells[position].Floor = null;
			if (Cells[position].Feature == null) {
				Cells.Remove(position);
			}
		}
		return obj;
	}
	
	public ShipObject RemoveFeature(Vector2 position) {
		ShipObject obj = null;
		if (Cells.ContainsKey(position)) {
			obj = Cells[position].Feature;
			Cells[position].Feature = null;
			if (Cells[position].Floor == null) {
				Cells.Remove(position);
			}
		}
		return obj;
	}
	
	public ShipObject SetFloor(Vector2 cellPos, Vector3 objectPos, GameObject gameObject) {
		ShipObject obj = new ShipObject(gameObject, objectPos);
		if (Cells.ContainsKey(cellPos)) {
			Cells[cellPos].Floor = obj;
		} else {
			ShipCell cell = new ShipCell() { Floor = obj };
			Cells.Add(cellPos, cell);
		}
		return obj;
	}
	
	public ShipObject SetFeature(Vector2 cellPos, Vector3 objectPos, GameObject gameObject) {
		ShipObject obj = new ShipObject(gameObject, objectPos);
		if (Cells.ContainsKey(cellPos)) {
			Cells[cellPos].Feature = obj;
		} else {
			ShipCell cell = new ShipCell() { Feature = obj };
			Cells.Add(cellPos, cell);
		}
		return obj;
	}
}

public class SerializedShip {
	public List<SerializedCell> Cells { get; set; }
}

public class SerializedCell {
	public int Deck { get; set; }
	public float x { get; set; }
	public float y { get; set; }
	public SerializedObject Floor { get; set; }
	public SerializedObject Wall { get; set; }
}

public class SerializedObject {
	public string GameObject { get; set; }
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }
}

public class Ship {
	public List<ShipDeck> Decks;
	
	public GameObject BedObject { get; set; }
	public GameObject ConsoleObject { get; set; }
	public GameObject DoorObject { get; set; }
	public GameObject EngineObject { get; set; }
	public GameObject LifeSupportObject { get; set; }
	public GameObject ReactorObject { get; set; }
	public GameObject WeaponSystemObject { get; set; }
	
	public GameObject AtmosphereObject { get; set; }
	public GameObject BridgeObject { get; set; }
	public GameObject EngineeringObject { get; set; }
	public GameObject FloorObject { get; set; }
	public GameObject QuartersObject { get; set; }
	public GameObject ReactorRoomObject { get; set; }
	public GameObject TacticalObject { get; set; }
	
	public GameObject WallObject { get; set; }
	public GameObject HullObject { get; set; }
	
	public Ship() {
	}
	
	public void Build() {
		Decks = new List<ShipDeck>();
		Decks.Add(new ShipDeck());
	}
	
	public SerializedShip Save() {
		List<SerializedCell> allCells = new List<SerializedCell>();
		for (int ii = 0; ii < Decks.Count; ii++) {
			foreach (Vector2 key in Decks[ii].Cells.Keys) {
				ShipCell cell = Decks[ii].Cells[key];
				SerializedCell sc = new SerializedCell() { Deck = ii, x = key.x, y = key.y };
				if (cell.Floor != null) {
					sc.Floor = new SerializedObject() { GameObject = cell.Floor.GameObject.ToString(), x = cell.Floor.Position.x, y = cell.Floor.Position.y, z = cell.Floor.Position.z };
				}
				if (cell.Feature != null) {
					sc.Wall = new SerializedObject() { GameObject = cell.Feature.GameObject.ToString(), x = cell.Feature.Position.x, y = cell.Feature.Position.y, z = cell.Feature.Position.z };
				}
				allCells.Add(sc);
			}
		}
		return new SerializedShip() { Cells = allCells };
	}
	
	private GameObject GetObject(string objectString) {
		GameObject obj = null;
		switch (objectString) {
		case "Hull (UnityEngine.GameObject)":
			obj = HullObject;
			break;
		case "Wall (UnityEngine.GameObject)":
			obj = WallObject;
			break;
		case "Bed (UnityEngine.GameObject)":
			obj = BedObject;
			break;
		case "Console (UnityEngine.GameObject)":
			obj = ConsoleObject;
			break;
		case "Door (UnityEngine.GameObject)":
			obj = DoorObject;
			break;
		case "Engine (UnityEngine.GameObject)":
			obj = EngineObject;
			break;
		case "LifeSupport (UnityEngine.GameObject)":
			obj = LifeSupportObject;
			break;
		case "Reactor (UnityEngine.GameObject)":
			obj = ReactorObject;
			break;
		case "WeaponSystem (UnityEngine.GameObject)":
			obj = WeaponSystemObject;
			break;
		case "Atmosphere (UnityEngine.GameObject)":
			obj = AtmosphereObject;
			break;
		case "Bridge (UnityEngine.GameObject)":
			obj = BridgeObject;
			break;
		case "Engineering (UnityEngine.GameObject)":
			obj = EngineeringObject;
			break;
		case "Floor (UnityEngine.GameObject)":
			obj = FloorObject;
			break;
		case "Quarters (UnityEngine.GameObject)":
			obj = QuartersObject;
			break;
		case "ReactorRoom (UnityEngine.GameObject)":
			obj = ReactorRoomObject;
			break;
		case "Tactical (UnityEngine.GameObject)":
			obj = TacticalObject;
			break;
		}
		
		return obj;
	}
	
	public void Load(SerializedShip ship) {
		Decks = new List<ShipDeck>();
		foreach (SerializedCell cell in ship.Cells) {
			if (Decks.Count <= cell.Deck) {
				for (int ii = Decks.Count; ii <= cell.Deck; ii++) {
					Decks.Add(new ShipDeck());
				}
			}
			Vector2 pos = new Vector3(cell.x, cell.y);
			if (cell.Floor != null) {
				GameObject obj = GetObject(cell.Floor.GameObject);
				if (obj != null) {
					Decks[cell.Deck].SetFloor(pos, new Vector3(cell.Floor.x, cell.Floor.y, cell.Floor.z), obj);
				}
			}
			if (cell.Wall != null) {
				GameObject obj = GetObject(cell.Wall.GameObject);
				if (obj != null) {
					Decks[cell.Deck].SetFeature(pos, new Vector3(cell.Wall.x, cell.Wall.y, cell.Wall.z), obj);
				}
			}
		}
	}
}
