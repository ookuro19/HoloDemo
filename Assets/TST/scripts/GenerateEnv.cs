using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateEnv : MonoBehaviour {

	public List<GameObject> EnvGOList;

	private float randomRange;
	private Camera mainCamera;
	private Ray ray;
	private static RaycastHit targetHitInfo;
	private Vector3 placementPosition;
	private Quaternion placementRotation;
	private int tstLayerMask;

	// Use this for initialization
	void Start () {
		mainCamera = Camera.main;
		tstLayerMask = 1 << LayerMask.NameToLayer ("Plane");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButton(0))
		{
			ray = mainCamera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out targetHitInfo,30f,tstLayerMask))
			{
				//使薄片的Z轴与射线检测的法向量平行
				Quaternion.FromToRotation(Vector3.forward, targetHitInfo.normal);
				placementPosition = targetHitInfo.point;
				placementRotation = Quaternion.FromToRotation(Vector3.forward, targetHitInfo.normal);
				GenerateEnvPlants (placementPosition, placementRotation);
			}
		}


	}

	private void GenerateEnvPlants(Vector3 tPosition, Quaternion tRotation)
	{
		//Check if there is a object
		if (IsSurroundingGOExist(tPosition)) {
			Instantiate (EnvGOList [Random.Range (0, EnvGOList.Count)], tPosition, tRotation);
		
		}
	}

	private bool IsSurroundingGOExist(Vector3 tPosition)
	{
		randomRange = Random.Range (1f, 3f);
		Debug.Log ("randomRange is " + randomRange);
		if (!Physics.Raycast(tPosition,Vector3.up,randomRange)) {
			Debug.DrawRay(tPosition, Vector3.up * randomRange, Color.red);

			if (!Physics.Raycast(tPosition,Vector3.down,randomRange)) {
				Debug.DrawRay(tPosition, Vector3.down * randomRange, Color.red);

				if (!Physics.Raycast(tPosition,Vector3.right,randomRange)) {
					Debug.DrawRay(tPosition, Vector3.right * randomRange, Color.red);

					if (!Physics.Raycast(tPosition,Vector3.left,randomRange)) {
						Debug.DrawRay(tPosition, Vector3.left * randomRange, Color.red);

						return true;
					}
				}
			}
		}
		return false;
	}

}
