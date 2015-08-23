using UnityEngine;
using UnityEngine.Networking;
using System.Collections;


public class GameController : MonoBehaviour {

	[SerializeField]
	GameObject Ghost;
	// Use this for initialization
	void Start () {
		var netManage = GameObject.Find ("NetworkManager").gameObject.GetComponent<NetworkManager> ();
		netManage.StartServer ();
		CreateGhosts();
	}

	void CreateGhosts() {
		var cntKey = "KEY_COUNT";
		var savecnt = PlayerPrefs.GetInt( cntKey, 0 );
		for ( int i = 0; i < savecnt && i < 20; i++ ) {
			var json = PlayerPrefs.GetString(i.ToString());
			var ghost = Instantiate( Ghost );
			ghost.GetComponent<NetCube>().autoFall.PlayTimeLine( json );
		}
	}

	// Update is called once per frame
	void Update () {
		if( Input.GetKey( KeyCode.Escape ) ) {
			Application.LoadLevel( "Main" ) ;
		}
	}

	void GameEnd () {
		var netManage = GameObject.Find ("NetworkManager").gameObject.GetComponent<NetworkManager> ();
		netManage.StopServer ();
	}
}
