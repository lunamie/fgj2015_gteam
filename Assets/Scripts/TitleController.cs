using UnityEngine;
using System.Collections;

public class TitleController : MonoBehaviour {

	[SerializeField] private bool _isServer ;

	// Use this for initialization
	void Start () {

	#if UNITY_EDITOR
		_isServer = true ;
	#else
		_isServer = false ;
	#endif
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToMainGame () {
		if (_isServer) {
			Application.LoadLevel( "Main" ) ;
		}
		else {
			Application.LoadLevel( "dev_Player_Ayaki" ) ;
		}
	}
}
