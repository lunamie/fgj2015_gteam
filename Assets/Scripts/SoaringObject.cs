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
		ParticleSystem ps = collider.gameObject.GetComponent<ParticleSystem>();
		if(collider.gameObject.tag == "Player" && ps != null && ps.isPlaying)
		{
			//Hit!

			//Destroy(this.gameObject);

			// Call Jump to Player

			PlayerSync sync = collider.gameObject.transform.parent.GetComponent<PlayerSync>();
			if(sync != null)
			{
				sync.CmdSoar(true);
			}
		}
	}
}
