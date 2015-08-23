using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class GameController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var netManage = GameObject.Find ("NetworkManager").gameObject.GetComponent<NetworkManager> ();
		netManage.StartServer ();

	}

	// Update is called once per frame
	void Update () {
		if( Input.GetKey( KeyCode.Escape ) ) {
			Application.LoadLevel( "Main" ) ;
		}
	}
}
