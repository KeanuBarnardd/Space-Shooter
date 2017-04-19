using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cruiserWaypoints : MonoBehaviour {

    //Variables
    public static Transform[] points;

    private void Awake()
    {
        points = new Transform[transform.childCount];
        //Go through and find each of the Children in they array
        for (int i = 0; i < points.Length; i++)
        {
            points[i] = transform.GetChild(i);
        }
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
