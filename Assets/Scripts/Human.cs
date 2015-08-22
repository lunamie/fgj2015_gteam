using UnityEngine;
using System.Collections;

public class Human : MonoBehaviour {

	[SerializeField]
	Texture[] _bodyTextures;
	[SerializeField]
	Texture[] _armTextures;

	[SerializeField]
	GameObject _body;
	[SerializeField]
	GameObject _armLeft;
	[SerializeField]
	GameObject _armRight;

	// Use this for initialization
	void Start () {

		 //Select Randam Index 
		int idx = Random.Range(0, _bodyTextures.Length); 
		 
		//Set Selected Textures 
		if(_body != null)
		{
			_body.GetComponent<Renderer>().material.mainTexture = _bodyTextures[idx];
		}
		if(_armLeft != null)
		{
			_armLeft.GetComponent<Renderer>().material.mainTexture = _armTextures[idx];
		}
		if(_armRight != null)
		{
			_armRight.GetComponent<Renderer>().material.mainTexture = _armTextures[idx];
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
