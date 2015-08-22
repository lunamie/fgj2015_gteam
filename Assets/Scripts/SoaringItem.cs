using UnityEngine;
using System.Collections;

public class SoaringItem : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter ( Collider coll ) {
		if (coll.gameObject.tag == "Player") {
			PlayerSync ps = coll.gameObject.transform.parent.gameObject.GetComponent<PlayerSync>() ;
			ps.CmdSoar( true ) ;

			Destroy( gameObject ) ;
		}
	}
}
