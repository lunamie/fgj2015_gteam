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

		 //Randam Animation and StartFrame
		Animation anim = this.gameObject.GetComponent<Animation>();
		int rand = Random.Range(0, 2);
		switch(rand)
		{
		case 0:
			anim.Play("humanAnime00");
			anim["humanAnime00"].time = Random.Range(0.0f, 1.0f);
			break;
		case 1:
			anim.Play("humanAnime02");
			anim["humanAnime02"].time = Random.Range(0.0f, 0.2f);
			break;
		default:
			anim.Play("humanAnime02");
			anim["humanAnime02"].time = Random.Range(0.0f, 0.2f);
			break;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
