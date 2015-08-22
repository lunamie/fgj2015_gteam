using UnityEngine;
using System.Collections;

public class SimpleLookAtCamera : MonoBehaviour {

	[SerializeField] private GameObject Target ;
	private float _fDiffPosY ;

	// Use this for initialization
	void Start () {
		_fDiffPosY = transform.localPosition.y - Target.transform.localPosition.y ;


	}
	
	// Update is called once per frame
	void Update () {
		transform.localPosition = new Vector3 (transform.localPosition.x, _fDiffPosY + Target.transform.localPosition.y, transform.localPosition.z);
	}
}
