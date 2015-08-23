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
	}

	public void CreateGhosts() {
		var cntKey = "KEY_COUNT";
		var savecnt = PlayerPrefs.GetInt( cntKey, 0 );
		for ( int i = savecnt - 1; i > 0 && i > savecnt - 21; i-- ) {
			var json = PlayerPrefs.GetString( i.ToString() );
			var ghost = Instantiate( Ghost );
			var component = ghost.GetComponent<NetCube>();
			component.autoFall.PlayTimeLine( json, () => {
				component.Move.enabled = false;
			} );
		}
	}

	// Update is called once per frame
	void Update () {
		if( Input.GetKey( KeyCode.Escape ) ) {
			Application.LoadLevel( "Main" ) ;
		}
	}

	public void GameEnd () {
		var netManage = GameObject.Find ("NetworkManager").gameObject.GetComponent<NetworkManager> ();
		netManage.StopServer ();
	}
}
