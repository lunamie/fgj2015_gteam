using UnityEngine;
using UnityEngine.Networking ;
using System.Collections;

public class PlayerSync : NetworkBehaviour {

	//	: PlayerRot.
	[SyncVar] private Quaternion syncPlayerRotation ;

	[SerializeField] Vector3 MyAcceleration ;

	private Vector3 myRot ;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotations ();

		//transform.rotation = syncPlayerRotation ;
	}

	[Command]
	void CmdRotateToSever ( Vector3 i_vRot ) {
		//Debug.Log ("OK  Y = " + i_vRot.y);

		//syncPlayerRotation.eulerAngles = i_vRot ;
	}
	
	[Client]
	void TransmitRotations ()
	{
		if (isLocalPlayer) {

			Quaternion gyroQt = Input.gyro.attitude;
			MyAcceleration = Input.acceleration;

			//myRot = new Vector3 (0.0f, 0.0f, MyAcceleration.y * 180.0f + 180.0f) ;


			//CmdRotateToSever( myRot ) ;
		}
	}
}
