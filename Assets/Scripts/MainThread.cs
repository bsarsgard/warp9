using UnityEngine;
using System.Collections;

public class MainThread : MonoBehaviour {
	public GameObject wallCube;
	public GameObject floorCube;
	public GameObject personCapsule;

	// Use this for initialization
	void Start () {
		for (int zz = 0; zz <= 4; zz++) {
			for (int xx = -3; xx <= 3; xx++) {
				Instantiate(floorCube, new Vector3(xx, 0, zz), Quaternion.identity);
			}
		}
		Instantiate(wallCube, new Vector3(-3, 1, 0), Quaternion.identity);
		Instantiate(wallCube, new Vector3(-2, 1, 0), Quaternion.identity);
		Instantiate(wallCube, new Vector3(-1, 1, 0), Quaternion.identity);
		Instantiate(wallCube, new Vector3(0, 1, 0), Quaternion.identity);
		Instantiate(wallCube, new Vector3(1, 1, 0), Quaternion.identity);
		Instantiate(wallCube, new Vector3(2, 1, 0), Quaternion.identity);
		Instantiate(wallCube, new Vector3(3, 1, 0), Quaternion.identity);
		Instantiate(wallCube, new Vector3(-3, 1, 1), Quaternion.identity);
		Instantiate(wallCube, new Vector3(3, 1, 1), Quaternion.identity);
		Instantiate(wallCube, new Vector3(-3, 1, 2), Quaternion.identity);
		Instantiate(wallCube, new Vector3(3, 1, 2), Quaternion.identity);
		Instantiate(wallCube, new Vector3(-3, 1, 3), Quaternion.identity);
		Instantiate(wallCube, new Vector3(3, 1, 3), Quaternion.identity);
		Instantiate(wallCube, new Vector3(-3, 1, 4), Quaternion.identity);
		Instantiate(wallCube, new Vector3(-2, 1, 4), Quaternion.identity);
		Instantiate(wallCube, new Vector3(-1, 1, 4), Quaternion.identity);
		Instantiate(wallCube, new Vector3(0, 1, 4), Quaternion.identity);
		Instantiate(wallCube, new Vector3(1, 1, 4), Quaternion.identity);
		Instantiate(wallCube, new Vector3(2, 1, 4), Quaternion.identity);
		Instantiate(wallCube, new Vector3(3, 1, 4), Quaternion.identity);
		
		Instantiate(personCapsule, new Vector3(0, 1, 2), Quaternion.identity);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
