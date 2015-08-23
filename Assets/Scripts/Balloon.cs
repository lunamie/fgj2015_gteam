using UnityEngine;
using System.Collections;

public class Balloon : MonoBehaviour {

	[SerializeField]
	GameObject _explosion;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
//		if(Input.GetKeyDown(KeyCode.A) && _explosion != null)
//		{
//			GameObject explosion = Instantiate(_explosion); 
//			Vector3 setPos = this.gameObject.transform.localPosition + this.gameObject.transform.parent.position;
//			setPos.y += 0.8f;
//			explosion.transform.position = setPos;
//		}
	}

	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Player")
		{
			//Hit!

			//Explosion
			if(_explosion != null)
			{
				GameObject explosion = (GameObject) Instantiate(_explosion, Vector3.zero,  Quaternion.identity); 
				Vector3 setPos = this.gameObject.transform.localPosition + this.gameObject.transform.parent.position;
				setPos.y += 0.8f;
				explosion.transform.position = setPos;
			}

			//Destroy This with Parent
			Destroy(this.gameObject.transform.parent.gameObject); 
		}
	}
}
