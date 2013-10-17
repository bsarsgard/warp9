using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Pathfinding.Serialization.JsonFx;

public class MainThread : MonoBehaviour {
	public GameObject BedObject;
	public GameObject ConsoleObject;
	public GameObject DoorObject;
	public GameObject EngineObject;
	public GameObject LifeSupportObject;
	public GameObject ReactorObject;
	public GameObject WeaponSystemObject;
	
	public GameObject AtmosphereObject;
	public GameObject BridgeObject;
	public GameObject EngineeringObject;
	public GameObject FloorObject;
	public GameObject QuartersObject;
	public GameObject ReactorRoomObject;
	public GameObject TacticalObject;
	
	public GameObject HullObject;
	public GameObject WallObject;
	
	public GameObject DeleteWallObject;
	public GameObject DeleteFloorObject;
	
	public GameObject PersonObject;
	
	private Vector2? dragCursorStart = null;
	private Vector2? dragCursorStop = null;
	private Dictionary<Vector3, GameObject> dragCursorObjects = new Dictionary<Vector3, GameObject>();
	
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
					if (value != DeleteWallObject && value != DeleteFloorObject) {
						cursor.renderer.material.color = new Color(0, 0, 1f, 0.66f);
					}
				}
				_activeCursor = value;
			}
		}
	}
	private GameObject cursor = null;
	private GameObject _activeFloorCursor;
	private GameObject ActiveFloorCursor {
		get {
			return _activeFloorCursor;
		}
		set {
			if (value != _activeFloorCursor) {
				if (floorCursor != null) {
					Destroy(floorCursor);
				}
				if (value != null) {
					floorCursor = (GameObject)Instantiate(value, new Vector3(0, 0, 0), Quaternion.identity);
					if (value != DeleteWallObject && value != DeleteFloorObject) {
						floorCursor.renderer.material.color = new Color(0, 0, 1f, 0.66f);
					}
				}
				_activeFloorCursor = value;
			}
		}
	}
	private GameObject floorCursor = null;
	
	private Ship playerShip;
	private ShipDeck playerShipDeck;
	
	// Gui
	private Rect menuBox;
	private int toolbarInt = 0;
	private int selectionGridInt = -1;
		
	string[] toolbarStrings = {"Ship", "Room", "Feat"};
	string[][] selectionStrings = new string[][] {
		new string[] {"None", "Clear", "Hull", "Wall"},
		new string[] {"None", "Floor", "Bridge", "Engineering", "Reactor Room", "Tactical", "Atmosphere", "Quarters"},
		new string[] {"None", "Door", "Console", "Engine", "Reactor", "Weapon System", "Life Support", "Bed"}
	};

	// Use this for initialization
	void Start () {
		// Load gameobjects
		HullObject = (GameObject)Resources.Load("Parts/Ship/Hull");
		WallObject = (GameObject)Resources.Load("Parts/Ship/Wall");
		AtmosphereObject = (GameObject)Resources.Load("Parts/Room/Atmosphere");
		BedObject = (GameObject)Resources.Load("Parts/Feature/Bed");
		BridgeObject = (GameObject)Resources.Load("Parts/Room/Bridge");
		ConsoleObject = (GameObject)Resources.Load("Parts/Feature/Console");
		DoorObject = (GameObject)Resources.Load("Parts/Feature/Door");
		EngineeringObject = (GameObject)Resources.Load("Parts/Room/Engineering");
		EngineObject = (GameObject)Resources.Load("Parts/Feature/Engine");
		FloorObject = (GameObject)Resources.Load("Parts/Room/Floor");
		LifeSupportObject = (GameObject)Resources.Load("Parts/Feature/LifeSupport");
		QuartersObject = (GameObject)Resources.Load("Parts/Room/Quarters");
		ReactorObject = (GameObject)Resources.Load("Parts/Feature/Reactor");
		ReactorRoomObject = (GameObject)Resources.Load("Parts/Room/ReactorRoom");
		TacticalObject = (GameObject)Resources.Load("Parts/Room/Tactical");
		WeaponSystemObject = (GameObject)Resources.Load("Parts/Feature/WeaponSystem");
		// set up the gui
		menuBox = new Rect(0, 0, 180, 250);
		
		playerShip = new Ship() {
			AtmosphereObject = AtmosphereObject,
			BedObject = BedObject,
			BridgeObject = BridgeObject,
			ConsoleObject = ConsoleObject,
			DoorObject = DoorObject,
			EngineeringObject = EngineeringObject,
			EngineObject = EngineObject,
			FloorObject = FloorObject,
			HullObject = HullObject,
			LifeSupportObject = LifeSupportObject,
			QuartersObject = QuartersObject,
			ReactorObject = ReactorObject,
			ReactorRoomObject = ReactorRoomObject,
			TacticalObject = TacticalObject,
			WallObject = WallObject,
			WeaponSystemObject = WeaponSystemObject
		};
		
		// json load
		if (File.Exists("save.txt")) {
			string json = File.ReadAllText("save.txt");
			SerializedShip serializedShip = JsonReader.Deserialize<SerializedShip>(json);
			playerShip.Load(serializedShip);
		} else {
			playerShip.Build();
		}
		
		foreach (ShipDeck deck in playerShip.Decks) {
			foreach (ShipCell cell in deck.Cells.Values) {
				if (cell.Floor != null) {
					cell.Floor.Instance = (GameObject)Instantiate(cell.Floor.GameObject, cell.Floor.Position, Quaternion.identity);
				}
				if (cell.Feature != null) {
					cell.Feature.Instance = (GameObject)Instantiate(cell.Feature.GameObject, cell.Feature.Position, Quaternion.identity);
				}
			}
		}
		playerShipDeck = playerShip.Decks[0];
		
		Instantiate(PersonObject, new Vector3(0, 0, 2), Quaternion.identity);
	}
	
	// Draw the UI
	void OnGUI () {
		// Make a background box
		GUI.Box(menuBox, "Build Menu");
		
		int t = GUI.Toolbar( new Rect(10, 30, 160, 20), toolbarInt, toolbarStrings);
		if (t != toolbarInt) {
			toolbarInt = t;
			selectionGridInt = 0;
		}
		selectionGridInt = GUI.SelectionGrid (new Rect (10, 60, 160, 180), selectionGridInt, selectionStrings[toolbarInt], 1);
		
		if (selectionGridInt >= 0) {
			switch (selectionStrings[toolbarInt][selectionGridInt]) {
			// Ship
			case "Wall":
				ActiveCursor = WallObject;
				ActiveFloorCursor = FloorObject;
				break;
			case "Hull":
				ActiveCursor = HullObject;
				ActiveFloorCursor = FloorObject;
				break;
			case "Clear":
				ActiveCursor = DeleteWallObject;
				ActiveFloorCursor = DeleteFloorObject;
				break;
			// Rooms
			case "Floor":
				ActiveCursor = null;
				ActiveFloorCursor = FloorObject;
				break;
			case "Bridge":
				ActiveCursor = null;
				ActiveFloorCursor = BridgeObject;
				break;
			case "Engineering":
				ActiveCursor = null;
				ActiveFloorCursor = EngineeringObject;
				break;
			case "Reactor Room":
				ActiveCursor = null;
				ActiveFloorCursor = ReactorRoomObject;
				break;
			case "Tactical":
				ActiveCursor = null;
				ActiveFloorCursor = TacticalObject;
				break;
			case "Atmosphere":
				ActiveCursor = null;
				ActiveFloorCursor = AtmosphereObject;
				break;
			case "Quarters":
				ActiveCursor = null;
				ActiveFloorCursor = QuartersObject;
				break;
			// Features
			case "Door":
				ActiveCursor = DoorObject;
				ActiveFloorCursor = FloorObject;
				break;
			case "Console":
				ActiveCursor = ConsoleObject;
				ActiveFloorCursor = null;
				break;
			case "Engine":
				ActiveCursor = EngineObject;
				ActiveFloorCursor = EngineeringObject;
				break;
			case "Reactor":
				ActiveCursor = ReactorObject;
				ActiveFloorCursor = ReactorRoomObject;
				break;
			case "Weapon System":
				ActiveCursor = WeaponSystemObject;
				ActiveFloorCursor = TacticalObject;
				break;
			case "Life Support":
				ActiveCursor = LifeSupportObject;
				ActiveFloorCursor = AtmosphereObject;
				break;
			case "Bed":
				ActiveCursor = BedObject;
				ActiveFloorCursor = QuartersObject;
				break;
			case "None":
			default:
				ActiveCursor = null;
				ActiveFloorCursor = null;
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
				Vector2 cellPos = new Vector2(Mathf.Round(pos.x), Mathf.Round(pos.z));
				// Check mouse
				if (Input.GetMouseButtonUp(0)) // LMB Clicked
				{
					if (dragCursorStart == null || dragCursorStop == null || dragCursorStart.Equals(dragCursorStop)) {
						PaintCursor(cellPos);
					} else {
						for (
							float xx = Mathf.Min(dragCursorStart.Value.x, dragCursorStop.Value.x);
							xx <= Mathf.Max(dragCursorStart.Value.x, dragCursorStop.Value.x);
							xx++
						) {
							for (
								float yy = Mathf.Min(dragCursorStart.Value.y, dragCursorStop.Value.y);
								yy <= Mathf.Max(dragCursorStart.Value.y, dragCursorStop.Value.y);
								yy++
							) {
								PaintCursor(new Vector2(xx, yy));
							}
						}
					}
					dragCursorStart = null;
					dragCursorStop = null;
					foreach (GameObject obj in dragCursorObjects.Values) {
						Destroy(obj);
					}
					dragCursorObjects.Clear();
				}
				else if (Input.GetMouseButton(0))
				{
					// Cursor is being dragged
					if (dragCursorStart == null) {
						dragCursorStart = cellPos;
					}
					if (dragCursorStop != cellPos) {
						// redraw the drag area
						dragCursorStop = cellPos;
						foreach (GameObject obj in dragCursorObjects.Values) {
							Destroy(obj);
						}
						dragCursorObjects.Clear();
						for (
							float xx = Mathf.Min(dragCursorStart.Value.x, dragCursorStop.Value.x);
							xx <= Mathf.Max(dragCursorStart.Value.x, dragCursorStop.Value.x);
							xx++
						) {
							for (
								float yy = Mathf.Min(dragCursorStart.Value.y, dragCursorStop.Value.y);
								yy <= Mathf.Max(dragCursorStart.Value.y, dragCursorStop.Value.y);
								yy++
							) {
								// Draw cursor
								if (ActiveCursor != null) {
									Vector3 key = new Vector3(xx, 0.01f, yy);
									GameObject obj = (GameObject)Instantiate(ActiveCursor, key, Quaternion.identity);
									if (ActiveCursor != DeleteWallObject && ActiveCursor) {
										obj.renderer.material.color = new Color(0, 0, 1f, 0.66f);
									}
									dragCursorObjects[key] = obj;
								}
								if (ActiveFloorCursor != null) {
									Vector3 key = new Vector3(xx, -1.49f, yy);
									GameObject obj = (GameObject)Instantiate(ActiveFloorCursor, key, Quaternion.identity);
									if (ActiveFloorCursor != DeleteFloorObject) {
										obj.renderer.material.color = new Color(0, 0, 1f, 0.66f);
									}
									dragCursorObjects[key] = obj;
								}
							}
						}
					}
				}
				else
				{
					DrawCursor(cellPos);
				}
				
			}
		}
	}
	
	void PaintCursor(Vector2 cellPos) {
		Vector3 cubePos = new Vector3(Mathf.Round(cellPos.x), 0f, Mathf.Round(cellPos.y));
		if (ActiveCursor == DeleteWallObject || (ActiveCursor == null && ActiveFloorCursor != null)) {
			// Remove object at cursor
			ShipObject cube = playerShipDeck.RemoveFeature(cellPos);
			if (cube != null) {
				Destroy(cube.Instance);
			}
		} else if (ActiveCursor != null) {
			// Remove previous objects at cursor
			ShipObject cube = playerShipDeck.RemoveFeature(cellPos);
			if (cube != null) {
				Destroy(cube.Instance);
			}
			// Add object at cursor
			cube = playerShipDeck.SetFeature(cellPos, cubePos, ActiveCursor);
			cube.Instance = (GameObject)Instantiate(cube.GameObject, cubePos, Quaternion.identity);
		}
		Vector3 floorPos = new Vector3(cubePos.x, cubePos.y - 1.5f, cubePos.z);
		if (ActiveFloorCursor == DeleteFloorObject) {
			// Remove object at cursor
			ShipObject cube = playerShipDeck.RemoveFloor(cellPos);
			if (cube != null) {
				Destroy(cube.Instance);
			}
		} else if (ActiveFloorCursor != null) {
			// Remove previous objects at cursor
			ShipObject cube = playerShipDeck.RemoveFloor(cellPos);
			if (cube != null) {
				Destroy(cube.Instance);
			}
			// Add object at cursor
			cube = playerShipDeck.SetFloor(cellPos, floorPos, ActiveFloorCursor);
			cube.Instance = (GameObject)Instantiate(cube.GameObject, floorPos, Quaternion.identity);
		}
	}
	
	void DrawCursor(Vector2 pos) {
		// Draw cursor
		if (cursor != null) {
			cursor.transform.position = new Vector3(Mathf.Round(pos.x), 0.01f, Mathf.Round(pos.y));
		}
		if (floorCursor != null) {
			floorCursor.transform.position = new Vector3(Mathf.Round(pos.x), -1.49f, Mathf.Round(pos.y));
		}
	}
	
	void OnApplicationQuit() {
		playerShip.Save();
		string json = JsonWriter.Serialize(playerShip.Save());
		File.WriteAllText("save.txt", json);
	}
}

