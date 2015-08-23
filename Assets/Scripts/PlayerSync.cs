using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking ;
using System.Collections;

public class PlayerSync : NetworkBehaviour {

	//[SerializeField] private Text debugText ;
	[SerializeField] private float _fRotRatio ;
	[SerializeField] private float _fPosRatio ;
	[SerializeField] private float _fHeightRatio ;

	[SerializeField] private float _fSoaringPower ;

	[SerializeField] private GameObject _goModel ;
	[SerializeField] private Animator _animModel ;

	[SyncVar] private Quaternion syncPlayerRotation ;
	[SyncVar] private Vector3 syncPlayerPosition ;
	[SyncVar] private float syncPlayerSoaring ;

	[SerializeField] Vector3 MyAcceleration ;

	private Vector3 myRot ;
	private Vector3 myPos ;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("debugText") as GameObject;

		if (go) {
			//debugText = go.GetComponent<Text> ();
		}

		_animModel = _goModel.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotations ();

		//_goModel.transform.lo = syncPlayerRotation ;
		_goModel.transform.localPosition = new Vector3( syncPlayerPosition.x, _goModel.transform.localPosition.y + syncPlayerPosition.y + syncPlayerSoaring, _goModel.transform.localPosition.z ) ;
	}

	void DEBUG_OUTPUT () {
		Debug.Log ("Out");
	}

	void SoaringForItem () {
		if (_animModel.GetBool ("isSoar")) {
			syncPlayerSoaring = _fSoaringPower ;
		}
		else {
			syncPlayerSoaring = 0.0f ;
		}
	}

	[Command]
	void CmdJump ( bool isJumped ) {
		_animModel.SetBool( "isJump", isJumped ) ;
	}
	
	[Command]
	public void CmdSoar ( bool isSoar ) {
		_animModel.SetBool( "isSoar", isSoar ) ;

		//	: Provision.
		Invoke ("CmdDisenabledSoar", 1.0f);
	}

	//DEBUG.
	[Command]
	public void CmdDisenabledSoar () {
		_animModel.SetBool( "isSoar", false ) ;
	}


	[Command]
	void CmdTransformToSever ( Vector3 i_vRot, Vector3 i_vPos, Vector3 i_vAcc ) {
		//Debug.Log ("CmdRotateToSever OK  Y = " + i_vRot.z);

		syncPlayerRotation.eulerAngles = i_vRot ;
		syncPlayerPosition = i_vPos;

		Debug.Log ("X : " + i_vAcc.x + "  Y : " + i_vAcc.y + "  Z : " + i_vAcc.z);

	}
	
	[Client]
	void TransmitRotations ()
	{
		if (isLocalPlayer) {

			Quaternion gyroQt = Input.gyro.attitude;
			MyAcceleration = Input.acceleration;

			Vector3 vPos = new Vector3(0.0f, 0.0f, 0.0f) ;
			Vector3 vRot = new Vector3(0.0f, 0.0f, MyAcceleration.x * _fRotRatio * (-1) ) ;
			//myRot = new Vector3 (0.0f, 0.0f, MyAcceleration.x * _fRotRatio * (-1) ) ;

			float fAccMag = MyAcceleration.magnitude ;
			if( MyAcceleration.y <= -0.3f ) {//&& MyAcceleration.z > -0.8f && MyAcceleration.z < -1.2f ) {
				vPos = new Vector3( MyAcceleration.x * _fPosRatio, 1.0f * _fHeightRatio, _goModel.transform.localPosition.z ) ;
			}
			else {
				vPos = new Vector3( MyAcceleration.x * _fPosRatio, 0.0f, _goModel.transform.localPosition.z ) ;
			}

			if( MyAcceleration.z <= -1.2f ) {
				CmdJump ( true ) ;
			}
			else {
				CmdJump ( false ) ;
			}

			//debugText.text = "Rotate Y : " + MyAcceleration.y.ToString() ;

			SoaringForItem() ;

			DEBUG_OUTPUT() ;

			CmdTransformToSever( vRot, vPos, MyAcceleration ) ;

			myPos = vPos ;
			myRot = vRot ;
		}
	}
}
