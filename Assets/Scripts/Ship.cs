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

public class Ship {
	public Dictionary<Vector3, ShipCube> Cubes;
	
	public GameObject FloorCube { get; set; }
	public GameObject WallCube { get; set; }
	public GameObject HullCube { get; set; }
	
	public Ship(GameObject floorCube, GameObject hullCube, GameObject wallCube) {
		Cubes = new Dictionary<Vector3, ShipCube>();
		FloorCube = floorCube;
		HullCube = hullCube;
		WallCube = wallCube;
		for (int zz = 0; zz <= 4; zz++) {
			for (int xx = -3; xx <= 3; xx++) {
				Cubes.Add(new Vector3(xx, 0, zz), new ShipCube(FloorCube));
			}
		}
		
		Cubes.Add(new Vector3(-4, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-3, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-2, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-1, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(0, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(1, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(2, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(3, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(4, 1, -1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-4, 1, 0), new ShipCube(HullCube));
		Cubes.Add(new Vector3(4, 1, 0), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-4, 1, 1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(4, 1, 1), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-4, 1, 2), new ShipCube(HullCube));
		Cubes.Add(new Vector3(4, 1, 2), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-4, 1, 3), new ShipCube(HullCube));
		Cubes.Add(new Vector3(4, 1, 3), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-4, 1, 4), new ShipCube(HullCube));
		Cubes.Add(new Vector3(4, 1, 4), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-4, 1, 5), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-3, 1, 5), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-2, 1, 5), new ShipCube(HullCube));
		Cubes.Add(new Vector3(-1, 1, 5), new ShipCube(HullCube));
		Cubes.Add(new Vector3(0, 1, 5), new ShipCube(HullCube));
		Cubes.Add(new Vector3(1, 1, 5), new ShipCube(HullCube));
		Cubes.Add(new Vector3(2, 1, 5), new ShipCube(HullCube));
		Cubes.Add(new Vector3(3, 1, 5), new ShipCube(HullCube));
		Cubes.Add(new Vector3(4, 1, 5), new ShipCube(HullCube));
		
		Cubes.Add(new Vector3(-3, 1, 0), new ShipCube(WallCube));
		Cubes.Add(new Vector3(-2, 1, 0), new ShipCube(WallCube));
		Cubes.Add(new Vector3(-1, 1, 0), new ShipCube(WallCube));
		Cubes.Add(new Vector3(0, 1, 0), new ShipCube(WallCube));
		Cubes.Add(new Vector3(1, 1, 0), new ShipCube(WallCube));
		Cubes.Add(new Vector3(2, 1, 0), new ShipCube(WallCube));
		Cubes.Add(new Vector3(3, 1, 0), new ShipCube(WallCube));
		Cubes.Add(new Vector3(-3, 1, 1), new ShipCube(WallCube));
		Cubes.Add(new Vector3(3, 1, 1), new ShipCube(WallCube));
		Cubes.Add(new Vector3(-3, 1, 2), new ShipCube(WallCube));
		Cubes.Add(new Vector3(3, 1, 2), new ShipCube(WallCube));
		Cubes.Add(new Vector3(-3, 1, 3), new ShipCube(WallCube));
		Cubes.Add(new Vector3(3, 1, 3), new ShipCube(WallCube));
		Cubes.Add(new Vector3(-3, 1, 4), new ShipCube(WallCube));
		Cubes.Add(new Vector3(-2, 1, 4), new ShipCube(WallCube));
		Cubes.Add(new Vector3(-1, 1, 4), new ShipCube(WallCube));
		Cubes.Add(new Vector3(0, 1, 4), new ShipCube(WallCube));
		Cubes.Add(new Vector3(1, 1, 4), new ShipCube(WallCube));
		Cubes.Add(new Vector3(2, 1, 4), new ShipCube(WallCube));
		Cubes.Add(new Vector3(3, 1, 4), new ShipCube(WallCube));
	}
	
	
}
