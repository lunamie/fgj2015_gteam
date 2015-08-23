using UnityEngine;
using System.Collections;

public class SoaringObject : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Player" && this.gameObject.activeSelf)
		{
			//Hit!

			//Destroy(this.gameObject);

			// Call Jump to Player

			PlayerSync sync = collider.gameObject.transform.parent.GetComponent<PlayerSync>();
			if(sync != null)
			{
				Debug.Log("CallJump");
				sync.CmdSoar(true);
			}
		}
	}
}
