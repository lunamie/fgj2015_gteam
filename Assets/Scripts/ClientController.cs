using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ClientController : MonoBehaviour {

	[SerializeField] private NetworkManager _netManage ;

	// Use this for initialization
	void Start () {
		_netManage = GameObject.Find( "NetworkManager" ).gameObject.GetComponent<NetworkManager> ();
		_netManage.StartClient ();	
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (!_netManage.isNetworkActive) {
			Application.LoadLevel( "Title" ) ;
		}
	}
}
