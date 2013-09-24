using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipCube {
	public GameObject GameObject { get; set; }
	public GameObject Instance { get; set; }
	
	public ShipCube() {
	}
	
	public ShipCube(GameObject gameObject) {
		this.GameObject = gameObject;
	}
}

public class ShipDeck {
	public Dictionary<Vector3, ShipCube> Cubes;
	
	public ShipDeck() {
		Cubes = new Dictionary<Vector3, ShipCube>();
	}
	
	public ShipCube AddCube(Vector3 position, GameObject gameObject) {
		ShipCube cube = new ShipCube(gameObject);
		Cubes.Add(position, cube);
		return cube;
	}
	
	public ShipCube RemoveCube(Vector3 position) {
		ShipCube cube = null;
		if (Cubes.ContainsKey(position)) {
			cube = Cubes[position];
		}
		if (cube != null) {
			Cubes.Remove(position);
		}
		return cube;
	}
}

public class SerializedShip {
	public List<SerializedCube> Cubes { get; set; }
}

public class SerializedCube {
	public int Deck { get; set; }
	public string GameObject { get; set; }
	public float x { get; set; }
	public float y { get; set; }
	public float z { get; set; }
}

public class Ship {
	public List<ShipDeck> Decks;
	
	public GameObject FloorCube { get; set; }
	public GameObject WallCube { get; set; }
	public GameObject HullCube { get; set; }
	
	public Ship() {
	}
	
	public Ship(GameObject floorCube, GameObject hullCube, GameObject wallCube) {
		FloorCube = floorCube;
		HullCube = hullCube;
		WallCube = wallCube;
	}
	
	public void Build() {
		Decks = new List<ShipDeck>();
		Decks.Add(new ShipDeck());
		for (int zz = 0; zz <= 4; zz++) {
			for (int xx = -3; xx <= 3; xx++) {
				Decks[0].Cubes.Add(new Vector3(xx, 0, zz), new ShipCube(FloorCube));
			}
		}
	}
	
	public SerializedShip Save() {
		List<SerializedCube> allCubes = new List<SerializedCube>();
		for (int ii = 0; ii < Decks.Count; ii++) {
			foreach (Vector3 key in Decks[ii].Cubes.Keys) {
				ShipCube sc = Decks[ii].Cubes[key];
				SerializedCube cube = new SerializedCube() { Deck = ii, GameObject = sc.GameObject.ToString(), x = key.x, y = key.y, z = key.z };
				allCubes.Add(cube);
			}
		}
		return new SerializedShip() { Cubes = allCubes };
	}
	
	public void Load(SerializedShip ship) {
		Decks = new List<ShipDeck>();
		foreach (SerializedCube cube in ship.Cubes) {
			if (Decks.Count <= cube.Deck) {
				for (int ii = Decks.Count; ii <= cube.Deck; ii++) {
					Decks.Add(new ShipDeck());
				}
			}
			Vector3 vector = new Vector3(cube.x, cube.y, cube.z);
			GameObject obj = null;
			switch (cube.GameObject) {
			case "FloorCube (UnityEngine.GameObject)":
				obj = FloorCube;
				break;
			case "HullCube (UnityEngine.GameObject)":
				obj = HullCube;
				break;
			case "WallCube (UnityEngine.GameObject)":
				obj = WallCube;
				break;
			}
			Decks[cube.Deck].Cubes.Add(vector, new ShipCube(obj));
		}
	}
}
