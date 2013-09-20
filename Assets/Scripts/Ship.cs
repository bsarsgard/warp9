using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipCube {
	public GameObject GameObject { get; set; }
	public GameObject Instance { get; set; }
	
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

public class Ship {
	public List<ShipDeck> Decks;
	
	public GameObject FloorCube { get; set; }
	public GameObject WallCube { get; set; }
	public GameObject HullCube { get; set; }
	
	public Ship(GameObject floorCube, GameObject hullCube, GameObject wallCube) {
		FloorCube = floorCube;
		HullCube = hullCube;
		WallCube = wallCube;
		
		Decks = new List<ShipDeck>();
		Decks.Add(new ShipDeck());
		for (int yy = 0; yy <= 4; yy++) {
			for (int xx = -3; xx <= 3; xx++) {
				Decks[0].Cubes.Add(new Vector3(xx, yy, 0), new ShipCube(FloorCube));
			}
		}
		
		Decks[0].Cubes.Add(new Vector3(-4, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-3, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-2, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-1, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(0, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(1, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(2, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(3, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, -1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 0, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 0, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 1, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 2, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 2, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 3, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 3, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 4, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 4, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-3, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-2, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-1, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(0, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(1, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(2, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(3, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 5, 1), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-3, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-2, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-1, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(0, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(1, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(2, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(3, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, -1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 0, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 0, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 1, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 2, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 2, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 3, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 3, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 4, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 4, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-4, 5, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-3, 5, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-2, 5, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(-1, 5, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(0, 5, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(1, 5, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(2, 5, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(3, 5, 0), new ShipCube(HullCube));
		Decks[0].Cubes.Add(new Vector3(4, 5, 0), new ShipCube(HullCube));
		
		Decks[0].Cubes.Add(new Vector3(-3, 0, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(-2, 0, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(-1, 0, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(0, 0, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(1, 0, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(2, 0, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(3, 0, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(-3, 1, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(3, 1, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(-3, 2, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(3, 2, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(-3, 3, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(3, 3, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(-3, 4, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(-2, 4, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(-1, 4, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(0, 4, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(1, 4, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(2, 4, 1), new ShipCube(WallCube));
		Decks[0].Cubes.Add(new Vector3(3, 4, 1), new ShipCube(WallCube));
	}
}
