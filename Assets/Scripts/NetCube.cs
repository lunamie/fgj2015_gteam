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

	[SerializeField]
	Renderer modelRenderer;

	public AutoFall autoFall {
		get {
			return fall;
		}
	}

	public Move Move {
		get {
			return move;
		}
	}
	void Start () {
		var points = GameObject.Find( "MovePoints" );
		for ( int i = 0; i < points.transform.childCount; i++ ) {
			move.AddPoint( points.transform.GetChild( i ) );
		}

		Vector3 vInitPos =  points.transform.GetChild (0).transform.position;

		transform.position = new Vector3 (vInitPos.x, transform.position.y, vInitPos.z);
		var gameController = GameObject.FindObjectOfType<GameController>();
		if ( isGhost ) {
			modelRenderer.material = new Material( modelRenderer.material );
			modelRenderer.material.color = new Color( Random.Range( 0f, 1f ), Random.Range( 0f, 1f ), Random.Range( 0f, 1f ), 0.5f );

		}

		if ( !isGhost ) {
			gameController.CreateGhosts( );
			fall.StartFall( () => {
				Debug.Log( "おちちゃった…" );
				move.enabled = false;
				gameController.GameEnd();
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
