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

	[SerializeField] private float _fMinWobble ;
	[SerializeField] private float _fMaxWobble ;

	
	[SyncVar] private Quaternion syncPlayerRotation ;
	[SyncVar] private Vector3 syncPlayerPosition ;
	[SyncVar] private Vector3 syncPlayerAccele ;

	[SerializeField] private float syncPlayerSoaring ;

	private Vector3 myRot ;
	private Vector3 myPos ;

	// Use this for initialization
	void Start () {
		GameObject go = GameObject.Find ("debugText") as GameObject;

		if (go) {
			//debugText = go.GetComponent<Text> ();
		}

		_animModel = _goModel.GetComponent<Animator> ();

		_fMinWobble = 0.0f;
		_fMaxWobble = 0.0f;

#if UNITY_EDITOR
		syncPlayerRotation = transform.rotation;
#else
		Input.gyro.enabled = true;
#endif
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		TransmitRotations ();

		SoaringForItem() ;

		RunManage () ;

		//_goModel.transform.lo = syncPlayerRotation ;
		_goModel.transform.localPosition = new Vector3( syncPlayerPosition.x, _goModel.transform.localPosition.y + syncPlayerPosition.y + syncPlayerSoaring, _goModel.transform.localPosition.z ) ;
	}

	void DEBUG_OUTPUT () {
		Debug.Log ("Out");
	}

	void RunManage () {

		if (syncPlayerAccele.z <= -1.75f) {
			_fMinWobble = syncPlayerAccele.z ;
		}

		if (syncPlayerAccele.z >= -0.4f) {
			_fMaxWobble = syncPlayerAccele.z ;
		}

		if ( _fMinWobble < 0.0f && _fMaxWobble < 0.0f ) {
			_fMinWobble = 0.0f ;
			_fMaxWobble = 0.0f ;

			_goModel.transform.localPosition += new Vector3( 0.0f, 1.5f, 0.0f ) ;
		}
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
	public void CmdJump ( bool isJumped ) {
		_animModel.SetBool( "isJump", isJumped ) ;
	}
	
	//[Command]
	public void CmdSoar ( bool isSoar ) {
		_animModel.SetBool( "isSoar", isSoar ) ;

		//	: Provision.
		Invoke ("CmdDisenabledSoar", 1.0f);
	}

	//DEBUG.
	//[Command]
	public void CmdDisenabledSoar () {
		_animModel.SetBool( "isSoar", false ) ;
	}


	[Command]
	void CmdTransformToSever ( Vector3 i_vRot, Vector3 i_vPos, Vector3 i_vAcc ) {
		//Debug.Log ("CmdRotateToSever OK  Y = " + i_vRot.z);

		syncPlayerRotation.eulerAngles = i_vRot ;
		syncPlayerPosition = i_vPos;
		syncPlayerAccele = i_vAcc;

		Debug.Log ("X : " + i_vAcc.x + "  Y : " + i_vAcc.y + "  Z : " + i_vAcc.z);

	}
	
	[Client]
	void TransmitRotations ()
	{
		if (isLocalPlayer) {

			Quaternion gyroQt = Input.gyro.attitude;
			Vector3 vAcceleration = Input.acceleration;

			Vector3 vPos = new Vector3(0.0f, 0.0f, 0.0f) ;
			Vector3 vRot = new Vector3(0.0f, 0.0f, gyroQt.eulerAngles.x * _fRotRatio * (-1) ) ;
			//myRot = new Vector3 (0.0f, 0.0f, vAcceleration.x * _fRotRatio * (-1) ) ;

			float fAccMag = vAcceleration.magnitude ;

			if( vAcceleration.y <= -0.3f && vAcceleration.z > -0.8f && vAcceleration.z < -1.2f ) {
				vPos = new Vector3( vAcceleration.x * _fPosRatio, 1.0f * _fHeightRatio, _goModel.transform.localPosition.z ) ;
			}
			else {
				vPos = new Vector3( vAcceleration.x * _fPosRatio, 0.0f, _goModel.transform.localPosition.z ) ;
			}


			if( vAcceleration.z <= -2.8f ) {
				CmdJump ( true ) ;
			}
			else {
				CmdJump ( false ) ;
			}

			//debugText.text = "Rotate Y : " + vAcceleration.y.ToString() ;

			DEBUG_OUTPUT() ;

			CmdTransformToSever( vRot, vPos, vAcceleration ) ;

			myPos = vPos ;
			myRot = vRot ;
		}
	}

	/// <summary>
	/// Item Get
	/// </summary>
	//[Client]
	public void Soaring ()
	{
		_animModel.SetBool( "isSoar", true ) ;
		
		//	: Provision.
		Invoke ("CmdDisenabledSoar", 1.0f);
	}
}
