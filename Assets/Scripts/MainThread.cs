using UnityEngine;
using System.Collections;

public class MainThread : MonoBehaviour {
	public GameObject WallCube;
	public GameObject FloorCube;
	public GameObject HullCube;
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
					cursor.renderer.material.color = new Color(0, 0, 1f, 0.66f);
				}
				_activeCursor = value;
			}
		}
	}
	private GameObject cursor = null;
	
	// Gui
	private Rect menuBox;
	private int selectionGridInt = -1;
		
	string[] selectionStrings = {"Clear", "Hull", "Wall"};

	// Use this for initialization
	void Start () {
		for (int zz = 0; zz <= 4; zz++) {
			for (int xx = -3; xx <= 3; xx++) {
				Instantiate(FloorCube, new Vector3(xx, 0, zz), Quaternion.identity);
			}
		}
		
		Instantiate(HullCube, new Vector3(-4, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-3, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-2, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-1, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(0, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(1, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(2, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(3, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(4, 1, -1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-4, 1, 0), Quaternion.identity);
		Instantiate(HullCube, new Vector3(4, 1, 0), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-4, 1, 1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(4, 1, 1), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-4, 1, 2), Quaternion.identity);
		Instantiate(HullCube, new Vector3(4, 1, 2), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-4, 1, 3), Quaternion.identity);
		Instantiate(HullCube, new Vector3(4, 1, 3), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-4, 1, 4), Quaternion.identity);
		Instantiate(HullCube, new Vector3(4, 1, 4), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-4, 1, 5), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-3, 1, 5), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-2, 1, 5), Quaternion.identity);
		Instantiate(HullCube, new Vector3(-1, 1, 5), Quaternion.identity);
		Instantiate(HullCube, new Vector3(0, 1, 5), Quaternion.identity);
		Instantiate(HullCube, new Vector3(1, 1, 5), Quaternion.identity);
		Instantiate(HullCube, new Vector3(2, 1, 5), Quaternion.identity);
		Instantiate(HullCube, new Vector3(3, 1, 5), Quaternion.identity);
		Instantiate(HullCube, new Vector3(4, 1, 5), Quaternion.identity);
		
		Instantiate(WallCube, new Vector3(-3, 1, 0), Quaternion.identity);
		Instantiate(WallCube, new Vector3(-2, 1, 0), Quaternion.identity);
		Instantiate(WallCube, new Vector3(-1, 1, 0), Quaternion.identity);
		Instantiate(WallCube, new Vector3(0, 1, 0), Quaternion.identity);
		Instantiate(WallCube, new Vector3(1, 1, 0), Quaternion.identity);
		Instantiate(WallCube, new Vector3(2, 1, 0), Quaternion.identity);
		Instantiate(WallCube, new Vector3(3, 1, 0), Quaternion.identity);
		Instantiate(WallCube, new Vector3(-3, 1, 1), Quaternion.identity);
		Instantiate(WallCube, new Vector3(3, 1, 1), Quaternion.identity);
		Instantiate(WallCube, new Vector3(-3, 1, 2), Quaternion.identity);
		Instantiate(WallCube, new Vector3(3, 1, 2), Quaternion.identity);
		Instantiate(WallCube, new Vector3(-3, 1, 3), Quaternion.identity);
		Instantiate(WallCube, new Vector3(3, 1, 3), Quaternion.identity);
		Instantiate(WallCube, new Vector3(-3, 1, 4), Quaternion.identity);
		Instantiate(WallCube, new Vector3(-2, 1, 4), Quaternion.identity);
		Instantiate(WallCube, new Vector3(-1, 1, 4), Quaternion.identity);
		Instantiate(WallCube, new Vector3(0, 1, 4), Quaternion.identity);
		Instantiate(WallCube, new Vector3(1, 1, 4), Quaternion.identity);
		Instantiate(WallCube, new Vector3(2, 1, 4), Quaternion.identity);
		Instantiate(WallCube, new Vector3(3, 1, 4), Quaternion.identity);
		
		Instantiate(PersonCapsule, new Vector3(0, 1, 2), Quaternion.identity);
		
		// Create the cursor
		//cursor = (GameObject)Instantiate(WallCube, new Vector3(0, 0, 0), Quaternion.identity);
		//cursor.renderer.material.color = new Color(0, 0, 1f, 0.66f);
		
		// set up the gui
		menuBox = new Rect(10,10,100,200);
	}
	
	// Draw the UI
	void OnGUI () {
		// Make a background box
		GUI.Box(menuBox, "Build Menu");

		selectionGridInt = GUI.SelectionGrid (new Rect (20, 40, 80, 60), selectionGridInt, selectionStrings, 1);
		
		if (selectionGridInt >= 0) {
			switch (selectionStrings[selectionGridInt]) {
			case "Wall":
				ActiveCursor = WallCube;
				break;
			case "Hull":
				ActiveCursor = HullCube;
				break;
			case "Clear":
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
					// Add object at cursor
					Instantiate(ActiveCursor, new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z)), Quaternion.identity);
					/*
					Vector3 cubePoint = new Vector3(hit.point.x, hit.point.y, hit.point.z);
					cubePoint.y += 0.1f;
					cubePoint.x -= (cubePoint.x % terrainGenerator.TileWide) - terrainGenerator.TileWide / 2f;
					cubePoint.z -= (cubePoint.z % terrainGenerator.TileHigh) - terrainGenerator.TileHigh / 2f;
					
					Vector2 tilePoint = new Vector2(Mathf.FloorToInt(cubePoint.x / terrainGenerator.TileWide), Mathf.FloorToInt(cubePoint.z / terrainGenerator.TileHigh));
					Tile tile = null;
					
					switch (selectionStrings[selectionGridInt]) {
					case "Road":
						tile = new Road();
						break;
					case "Residential":
						tile = new Building();
						break;
					case "Clear":
						if (tileMap.ContainsKey(tilePoint)) {
							Destroy(tileMap[tilePoint].TileObject);
							tileMap.Remove(tilePoint);
						}
						break;
					default:
						break;
					}
					if (tile != null && !tileMap.ContainsKey(tilePoint)) {
						tile.TileObject = Instantiate(activeCursor, cubePoint, Quaternion.identity);
						tileMap.Add (tilePoint, tile);
					}
					*/
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

