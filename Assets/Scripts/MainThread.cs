using UnityEngine;
using System.Collections;

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
	
	// Gui
	private Rect menuBox;
	private int selectionGridInt = -1;
		
	string[] selectionStrings = {"None", "Clear", "Hull", "Wall"};

	// Use this for initialization
	void Start () {
		// set up the gui
		menuBox = new Rect(10,10,100,200);
		
		playerShip = new Ship(FloorCube, HullCube, WallCube);
		foreach (Vector3 key in playerShip.Cubes.Keys) {
			playerShip.Cubes[key].Instance = (GameObject)Instantiate(playerShip.Cubes[key].GameObject, key, Quaternion.identity);
		}
		
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
						if (playerShip.Cubes.ContainsKey(cubePos)) {
							Destroy(playerShip.Cubes[cubePos].Instance);
							playerShip.Cubes.Remove(cubePos);
						}
					} else if (ActiveCursor != null) {
						// Add object at cursor
						ShipCube cube = new ShipCube(ActiveCursor);
						if (playerShip.Cubes.ContainsKey(cubePos)) {
							Destroy(playerShip.Cubes[cubePos].Instance);
							cube.Instance = (GameObject)Instantiate(cube.GameObject, cubePos, Quaternion.identity);
							playerShip.Cubes[cubePos] = cube;
						} else {
							cube.Instance = (GameObject)Instantiate(cube.GameObject, cubePos, Quaternion.identity);
							playerShip.Cubes.Add(cubePos, cube);
						}
					}
				}
				else
				{
					// Draw cursor
					if (cursor != null) {
						cursor.transform.position = new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y) + 0.1f, Mathf.Round(pos.z));
					}
				}
				
			}
		}
		
		
	}
}

