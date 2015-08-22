using UnityEngine;
using System.Collections;

public class NetCube : MonoBehaviour {

	//public Vector3 MyAcceleration ;

	// Use this for initialization

	[SerializeField]
	Move move;

	void Start () {
		var points = GameObject.Find( "MovePoints" );
		for ( int i = 0; i < points.transform.childCount; i++ ) {
			move.AddPoint( points.transform.GetChild( i ) );
		}
	}
	
	// Update is called once per frame
	void Update () {
		/*
		Quaternion gyroQt = Input.gyro.attitude;
		MyAcceleration = Input.acceleration;

		transform.eulerAngles = new Vector3 (0.0f, 0.0f, MyAcceleration.y * 180.0f + 180.0f);
		*/
	}
}
