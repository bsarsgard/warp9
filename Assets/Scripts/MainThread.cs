using UnityEngine;
using System.Collections;
using System.IO;
using Pathfinding.Serialization.JsonFx;

public class MainThread : MonoBehaviour {
	public GameObject WallCube;
	public GameObject FloorCube;
	public GameObject HullCube;
	public GameObject DeleteCube;
	public GameObject PersonCapsule;
	
	private GameObject _activeCursor;
	private GameObject ActiveCursor {
		get {
			return _activeCursor;
		}
		set {
			if (value != _activeCursor) {
				if (cursor != null) {
					Destroy(cursor);
				}
				if (value != null) {
					cursor = (GameObject)Instantiate(value, new Vector3(0, 0, 0), Quaternion.identity);
					if (value != DeleteCube) {
						cursor.renderer.material.color = new Color(0, 0, 1f, 0.66f);
					}
				}
				_activeCursor = value;
			}
		}
	}
	private GameObject cursor = null;
	
	private Ship playerShip;
	private ShipDeck playerShipDeck;
	
	// Gui
	private Rect menuBox;
	private int selectionGridInt = -1;
		
	string[] selectionStrings = {"None", "Clear", "Hull", "Wall"};

	// Use this for initialization
	void Start () {
		// set up the gui
		menuBox = new Rect(0,0,100,200);
		
		playerShip = new Ship(FloorCube, HullCube, WallCube);
		// json load
		if (File.Exists("save.txt")) {
			string json = File.ReadAllText("save.txt");
			SerializedShip serializedShip = JsonReader.Deserialize<SerializedShip>(json);
			playerShip.Load(serializedShip);
		} else {
			playerShip.Build();
		}
		foreach (ShipDeck deck in playerShip.Decks) {
			foreach (Vector3 key in deck.Cubes.Keys) {
				deck.Cubes[key].Instance = (GameObject)Instantiate(deck.Cubes[key].GameObject, key, Quaternion.identity);
			}
		}
		playerShipDeck = playerShip.Decks[0];
		
		Instantiate(PersonCapsule, new Vector3(0, 1, 2), Quaternion.identity);
	}
	
	// Draw the UI
	void OnGUI () {
		// Make a background box
		GUI.Box(menuBox, "Build Menu");

		selectionGridInt = GUI.SelectionGrid (new Rect (20, 40, 80, 80), selectionGridInt, selectionStrings, 1);
		
		if (selectionGridInt >= 0) {
			switch (selectionStrings[selectionGridInt]) {
			case "Wall":
				ActiveCursor = WallCube;
				break;
			case "Hull":
				ActiveCursor = HullCube;
				break;
			case "Clear":
				ActiveCursor = DeleteCube;
				break;
			case "None":
			default:
				ActiveCursor = null;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		// GUI stuff
		// Get on-screen mouse transltion
		float ix = Input.mousePosition.x;
		float iy = Input.mousePosition.y;
		Vector3 transMouse = GUI.matrix.inverse.MultiplyPoint3x4(new Vector3(ix, Screen.height - iy, 1));
		
		// Execute UI if the mouse is not over the menu box
		if (!menuBox.Contains(new Vector2(transMouse.x, transMouse.y))) {
		    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// create a plane at 0,0,0 whose normal points to +Y:
			Plane hPlane = new Plane(Vector3.up, -1);
			// Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
			float distance = 0;
			// if the ray hits the plane...
			if (hPlane.Raycast(ray, out distance)) {
				// get the hit point:
				Vector3 pos = ray.GetPoint(distance);
				// Check mouse
				if (Input.GetMouseButtonUp(0)) // LMB Clicked
				{
					Vector3 cubePos = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
					if (ActiveCursor == DeleteCube) {
						// Remove object at cursor
						ShipCube cube = playerShipDeck.RemoveCube(cubePos);
						if (cube != null) {
							Destroy(cube.Instance);
							if (!playerShipDeck.Cubes.ContainsKey(new Vector3(cubePos.x - 1, cubePos.y - 1, cubePos.z))
								|| !playerShipDeck.Cubes.ContainsKey(new Vector3(cubePos.x + 1, cubePos.y - 1, cubePos.z))
								|| !playerShipDeck.Cubes.ContainsKey(new Vector3(cubePos.x, cubePos.y - 1, cubePos.z - 1))
								|| !playerShipDeck.Cubes.ContainsKey(new Vector3(cubePos.x, cubePos.y - 1, cubePos.z + 1))
							) {
								// remove the floor
								cube = playerShipDeck.RemoveCube(new Vector3(cubePos.x, cubePos.y - 1, cubePos.z));
								if (cube != null) {
									Destroy(cube.Instance);
								}
							}
						}
					} else if (ActiveCursor != null) {
						// Remove previous object at cursor
						ShipCube cube = playerShipDeck.RemoveCube(cubePos);
						if (cube != null) {
							Destroy(cube.Instance);
						}
						// Add object at cursor
						cube = playerShipDeck.AddCube(cubePos, ActiveCursor);
						cube.Instance = (GameObject)Instantiate(cube.GameObject, cubePos, Quaternion.identity);
						// check if floor should be added
						Vector3 floorPos = new Vector3(cubePos.x, cubePos.y - 1, cubePos.z);
						if (!playerShipDeck.Cubes.ContainsKey(floorPos)) {
							if (playerShipDeck.Cubes.ContainsKey(new Vector3(cubePos.x - 1, cubePos.y, cubePos.z))
								&& playerShipDeck.Cubes.ContainsKey(new Vector3(cubePos.x + 1, cubePos.y, cubePos.z))
								&& playerShipDeck.Cubes.ContainsKey(new Vector3(cubePos.x, cubePos.y, cubePos.z - 1))
								&& playerShipDeck.Cubes.ContainsKey(new Vector3(cubePos.x, cubePos.y, cubePos.z + 1))
							) {
								// area is interior, give it a floor
								cube = playerShipDeck.AddCube(floorPos, FloorCube);
								cube.Instance = (GameObject)Instantiate(cube.GameObject, floorPos, Quaternion.identity);
							} else {
								// area is exterior, give it aditional hull
								cube = playerShipDeck.AddCube(floorPos, HullCube);
								cube.Instance = (GameObject)Instantiate(cube.GameObject, floorPos, Quaternion.identity);
							}
						}
					}
				}
				else
				{
					// Draw cursor
					if (cursor != null) {
						cursor.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y) + 0.01f, Mathf.Round(pos.z));
					}
				}
				
			}
		}
	}
	
	void OnApplicationQuit() {
		playerShip.Save();
		string json = JsonWriter.Serialize(playerShip.Save());
		File.WriteAllText("save.txt", json);
	}
}

