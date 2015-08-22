using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking ;
using System.Collections;

public class PlayerSync : NetworkBehaviour {

	//[SerializeField] private Text debugText ;
	public float _fRotRatio ;
	public float _fPosRatio ;
	
	[SyncVar] private Quaternion syncPlayerRotation ;
	[SyncVar] private Vector3 syncPlayerPosition ;

	[SerializeField] Vector3 MyAcceleration ;

	private Vector3 myRot ;
	private Vector3 myPos ;

	// Use this for initialization
	void Start () {
		//GameObject go = GameObject.Find ("debugText") as GameObject;

		//if (go) {
			//debugText = go.GetComponent<Text> ();
		//}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotations ();

		transform.rotation = syncPlayerRotation ;
		transform.position = syncPlayerPosition ;
	}

	void DEBUG_OUTPUT () {
		Debug.Log ("Out");
	}

	[Command]
	void CmdTransformToSever ( Vector3 i_vRot, Vector3 i_vPos ) {
		//Debug.Log ("CmdRotateToSever OK  Y = " + i_vRot.z);

		syncPlayerRotation.eulerAngles = i_vRot ;

		syncPlayerPosition = i_vPos;
	}
	
	[Client]
	void TransmitRotations ()
	{
		if (isLocalPlayer) {

			Quaternion gyroQt = Input.gyro.attitude;
			MyAcceleration = Input.acceleration;

			myRot = new Vector3 (0.0f, 0.0f, MyAcceleration.x * _fRotRatio * (-1) ) ;

			myPos = new Vector3( MyAcceleration.x * _fPosRatio, transform.position.y, transform.position.z ) ;

			//debugText.text = "Rotate Y : " + MyAcceleration.y.ToString() ;

			DEBUG_OUTPUT() ;

			CmdTransformToSever( myRot, myPos ) ;
		}
	}
}
