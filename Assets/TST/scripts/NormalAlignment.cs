using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAlignment : MonoBehaviour {

    public GameObject alignedCube;
    private Camera mainCamera;
    private Ray ray;
    private RaycastHit hitInfo;
    private Vector3 alignVector;
    
    // Use this for initialization
    void Start () {
        mainCamera = Camera.main;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(0))
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hitInfo))
            {
                //使
                alignedCube.transform.rotation = Quaternion.FromToRotation(Vector3.forward, hitInfo.normal);
                Debug.DrawRay(hitInfo.point, hitInfo.normal, Color.red);
                Debug.Log(string.Format("{0},{1},{2}", hitInfo.normal.x, hitInfo.normal.y, hitInfo.normal.z));

            }
        }
    }
}
