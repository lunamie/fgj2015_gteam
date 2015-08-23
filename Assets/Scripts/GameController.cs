using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Collections;


public class GameController : MonoBehaviour {

	[SerializeField]
	GameObject Ghost;

	[SerializeField]
	Text TimeText ;

	[SerializeField]
	float TimeCount ;

	// Use this for initialization
	void Start () {
		var netManage = GameObject.Find ("NetworkManager").gameObject.GetComponent<NetworkManager> ();
		netManage.StartServer ();
	}

	public void CreateGhosts(System.Action _callback) {
		var cntKey = "KEY_COUNT";
		var savecnt = PlayerPrefs.GetInt( cntKey, 0 );
		for ( int i = savecnt-1; i > 0 && i > savecnt - 21; i-- ) {
			var json = PlayerPrefs.GetString(i.ToString());
			var ghost = Instantiate( Ghost );
			ghost.GetComponent<NetCube>().autoFall.PlayTimeLine( json ,_callback);
		}
	}

	// Update is called once per frame
	void Update () {
		if( Input.GetKey( KeyCode.Escape ) ) {
			Application.LoadLevel( "Main" ) ;
		}

		// TimeCounting.
		TimeCount += Time.deltaTime;
		TimeText.text = Mathf.FloorToInt( TimeCount ) + " sec";
	}

	public void GameEnd () {
		var netManage = GameObject.Find ("NetworkManager").gameObject.GetComponent<NetworkManager> ();
		netManage.StopServer ();
	}
}
