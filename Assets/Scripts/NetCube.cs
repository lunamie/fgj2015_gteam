﻿using UnityEngine;
using System.Collections;

public class NetCube : MonoBehaviour {

	//public Vector3 MyAcceleration ;

	// Use this for initialization

	[SerializeField]
	Move move;

	[SerializeField]
	AutoFall fall;

	[SerializeField]
	bool isGhost;

	public AutoFall autoFall {
		get {
			return fall;
		}
	}

	void Start () {
		var points = GameObject.Find( "MovePoints" );
		for ( int i = 0; i < points.transform.childCount; i++ ) {
			move.AddPoint( points.transform.GetChild( i ) );
		}

		Vector3 vInitPos =  points.transform.GetChild (0).transform.position;

		transform.position = new Vector3 (vInitPos.x, transform.position.y, vInitPos.z);

		if ( !isGhost ) {
			fall.StartFall( () => {
				Debug.Log( "おちちゃった…" );
				move.enabled = false;
			GameObject.Find( "GameController" ).gameObject.SendMessage( "GameEnd" ) ;
			Application.LoadLevel( "Title" ) ;
			} );
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
