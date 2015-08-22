using UnityEngine;
using System.Collections;

public class Obstruct : MonoBehaviour {

	public float _fVelocity ;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, 3.0f);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate (0.0f, 0.0f, -_fVelocity);
	}
}
