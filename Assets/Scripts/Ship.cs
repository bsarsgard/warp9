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

public class ShipCell {
	public ShipObject Floor { get; set; }
	public ShipObject Wall { get; set; }
	public List<ShipObject> Features { get; set; }
}

public class ShipDeck {
	public Dictionary<Vector2, ShipCell> Cells;
	
	public ShipDeck() {
		Cells = new Dictionary<Vector2, ShipCell>();
	}
	
	/*
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
	*/
	
	public ShipObject RemoveFloor(Vector2 position) {
		ShipObject obj = null;
		if (Cells.ContainsKey(position)) {
			obj = Cells[position].Floor;
			Cells[position].Floor = null;
			if (Cells[position].Wall == null) {
				Cells.Remove(position);
			}
		}
		return obj;
	}
	
	public ShipObject RemoveWall(Vector2 position) {
		ShipObject obj = null;
		if (Cells.ContainsKey(position)) {
			obj = Cells[position].Wall;
			Cells[position].Wall = null;
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
	
	public ShipObject SetWall(Vector2 cellPos, Vector3 objectPos, GameObject gameObject) {
		ShipObject obj = new ShipObject(gameObject, objectPos);
		if (Cells.ContainsKey(cellPos)) {
			Cells[cellPos].Wall = obj;
		} else {
			ShipCell cell = new ShipCell() { Wall = obj };
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
	}
	
	public SerializedShip Save() {
		List<SerializedCell> allCells = new List<SerializedCell>();
		for (int ii = 0; ii < Decks.Count; ii++) {
			/*
			foreach (Vector3 key in Decks[ii].Objects.Keys) {
				ShipObject sc = Decks[ii].Objects[key];
				SerializedObject cube = new SerializedObject() { Deck = ii, GameObject = sc.GameObject.ToString(), x = key.x, y = key.y, z = key.z };
				allObjects.Add(cube);
			}
			*/
			foreach (Vector2 key in Decks[ii].Cells.Keys) {
				ShipCell cell = Decks[ii].Cells[key];
				SerializedCell sc = new SerializedCell() { Deck = ii, x = key.x, y = key.y };
				if (cell.Floor != null) {
					sc.Floor = new SerializedObject() { GameObject = cell.Floor.GameObject.ToString(), x = cell.Floor.Position.x, y = cell.Floor.Position.y, z = cell.Floor.Position.z };
				}
				if (cell.Wall != null) {
					sc.Wall = new SerializedObject() { GameObject = cell.Wall.GameObject.ToString(), x = cell.Wall.Position.x, y = cell.Wall.Position.y, z = cell.Wall.Position.z };
				}
				allCells.Add(sc);
			}
		}
		return new SerializedShip() { Cells = allCells };
	}
	
	private GameObject GetObject(string objectString) {
		GameObject obj = null;
		switch (objectString) {
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
			//Decks[cube.Deck].Objects.Add(vector, new ShipObject(obj));
			if (cell.Floor != null) {
				Decks[cell.Deck].SetFloor(pos, new Vector3(cell.Floor.x, cell.Floor.y, cell.Floor.z), GetObject(cell.Floor.GameObject));
			}
			if (cell.Wall != null) {
				Decks[cell.Deck].SetWall(pos, new Vector3(cell.Wall.x, cell.Wall.y, cell.Wall.z), GetObject(cell.Wall.GameObject));
			}
		}
	}
}
