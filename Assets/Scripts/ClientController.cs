using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ClientController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var netManage = GameObject.Find( "NetworkManager" ).gameObject.GetComponent<NetworkManager> ();
		netManage.StartClient ();	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
