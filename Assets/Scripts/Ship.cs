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
	public Dictionary<string, ShipCube> Cubes_ForSave;
	
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
	
	public void Save() {
		Cubes_ForSave = new Dictionary<string, ShipCube>();
		foreach (Vector3 vect in Cubes.Keys) {
			Cubes_ForSave.Add(vect.x + "," + vect.y + "," + vect.z, Cubes[vect]);
		}
		Cubes = null;
	}
	
	public void Load() {
		Cubes = new Dictionary<Vector3, ShipCube>();
		foreach (string vstr in Cubes_ForSave.Keys) {
			string[] vstrspl = vstr.Split(',');
			Vector3 vect = new Vector3(float.Parse(vstrspl[0]), float.Parse(vstrspl[1]), float.Parse(vstrspl[2]));
			Cubes.Add(vect, Cubes_ForSave[vstr]);
		}
		Cubes_ForSave = null;
	}
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
	
	public void Save() {
		foreach (ShipDeck deck in Decks) {
			deck.Save();
		}
	}
	
	public void Load() {
		foreach (ShipDeck deck in Decks) {
			deck.Load();
		}
	}
}
