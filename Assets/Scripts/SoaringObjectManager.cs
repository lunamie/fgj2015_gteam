using UnityEngine;
using System.Collections;

public class SoaringObjectManager : MonoBehaviour {

	[SerializeField]
	GameObject[] _objects;
	
	float _time = 0.000f;
	int _curIdx;
	
	// Use this for initialization
	void Start () {
		 //AllEmitterStop!

		for(int i = 0; i < _objects.Length; i++)
		{
			SetStopObject(i); 
		}

		SetActiveObject(Random.Range(0, _objects.Length));

		_time = 0.000f;
	}
	
	// Update is called once per frame
	void Update () {
		_time += Time.deltaTime;

		if(_time >= 15.000f)
		{
			_time -= 15.000f;
			SetStopObject(_curIdx);
			SetActiveObject(Random.Range(0, _objects.Length));
		}
	}

	void SetActiveObject(int idx)
	{
		if(_curIdx == idx)
		{
			idx = Random.Range(0, _objects.Length);
		}
		_curIdx = idx;

		foreach (Transform child in _objects[_curIdx].transform)
		{
			//child is your child transform
			ParticleSystem ps = child.GetComponent<ParticleSystem>();
			if(ps != null)
			{
				ps.Play();
			}
			
			foreach (Transform mago in child.transform)
			{
				ParticleSystem psMago = mago.GetComponent<ParticleSystem>();
				if(psMago != null)
				{
					psMago.Play();
				}
			}
		}
	}

	void SetStopObject(int idx)
	{
//		_curIdx = idx;
		
		foreach (Transform child in _objects[idx].transform)
		{
			//child is your child transform
			ParticleSystem ps = child.GetComponent<ParticleSystem>();
			if(ps != null)
			{
				ps.Stop();
			}
			
			foreach (Transform mago in child.transform)
			{
				ParticleSystem psMago = mago.GetComponent<ParticleSystem>();
				if(psMago != null)
				{
					psMago.Stop();
				}
			}
		}
	}
}
