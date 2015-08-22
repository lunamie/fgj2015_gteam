using UnityEngine;
using System.Collections;

public class ObstructCreator : MonoBehaviour {

	public GameObject _goObstruct ;

	public float _fIntervalTime ;
	public float _fIntervalTimeCount ;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		_fIntervalTimeCount += Time.deltaTime;

		if (_fIntervalTimeCount >= _fIntervalTime) {

			_fIntervalTimeCount = 0.0f ;

			Vector3 vPos = new Vector3 (Random.Range (-4.0f, 4.0f), 0.0f, 55);

			Instantiate (_goObstruct, vPos, Quaternion.identity);
		}
	}
}
