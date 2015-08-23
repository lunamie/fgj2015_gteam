using UnityEngine;
using System.Collections;

public class SoaringObjectManager : MonoBehaviour {

	[SerializeField]
	GameObject[] _objects = new GameObject[3];
	
	float _time = 0.000f;
	
	// Use this for initialization
	void Start () {
		_objects[1].SetActive(false);
		_objects[2].SetActive(false);
		
		_time = 0.000f;
	}
	
	// Update is called once per frame
	void Update () {
		_time += Time.deltaTime;
		if(_time > 45.000f) _time -= 45.000f;
		
		if(_time >= 0.000f && _time <= 15.000f)
		{
			if(_objects[0].activeSelf == false)
			{
				_objects[0].SetActive(true); 
			}
			if(_objects[2].activeSelf)
			{
				_objects[2].SetActive(false); 
			}
		}
		else if(_time >= 15.000f && _time <= 30.000f)
		{
			if(_objects[1].activeSelf == false)
			{
				_objects[1].SetActive(true); 
			}
			if(_objects[0].activeSelf)
			{
				_objects[0].SetActive(false); 
			}
		}
		else if(_time >= 30.000f && _time <= 45.000f)
		{
			if(_objects[2].activeSelf == false)
			{
				_objects[2].SetActive(true); 
			}
			if(_objects[1].activeSelf)
			{
				_objects[1].SetActive(false); 
			}
		}
	}
}
