using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking ;
using System.Collections;

public class PlayerSync : NetworkBehaviour {

	[SerializeField] private Text debugText ;
	public float _fRatio ;

	//	: PlayerRot.
	[SyncVar] private Quaternion syncPlayerRotation ;

	[SerializeField] Vector3 MyAcceleration ;

	private Vector3 myRot ;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("debugText") as GameObject;

		if (go) {
			debugText = go.GetComponent<Text> ();
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotations ();

		transform.rotation = syncPlayerRotation ;
	}

	[Command]
	void CmdRotateToSever ( Vector3 i_vRot ) {
		//Debug.Log ("CmdRotateToSever OK  Y = " + i_vRot.z);

		syncPlayerRotation.eulerAngles = i_vRot ;
	}
	
	[Client]
	void TransmitRotations ()
	{
		if (isLocalPlayer) {

			Quaternion gyroQt = Input.gyro.attitude;
			MyAcceleration = Input.acceleration;

			myRot = new Vector3 (0.0f, 0.0f, MyAcceleration.y) ;

			debugText.text = "Rotate Y : " + MyAcceleration.y.ToString() ;

			CmdRotateToSever( myRot ) ;
		}
	}
}
