using UnityEngine;
using System.Collections;

public class ParticleEffect : MonoBehaviour {

	ParticleSystem myParticleSystem = null;
	
	void Start ()
	{
		myParticleSystem = this.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (myParticleSystem != null && myParticleSystem.particleCount == 0)
		{
			Invoke("DestroyObject", 1.8f);
		}
	}

	void DestroyObject()
	{
		Destroy(this.gameObject);
	}
}
