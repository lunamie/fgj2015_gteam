using UnityEngine;
using System.Collections;

public class AutoFall : MonoBehaviour {

	[SerializeField]
	float _speed = 0.1f;

	/// <summary>
	/// 落下速度
	/// </summary>
	public float speed {
		get { return _speed; }
		set { _speed = value; }
	}


	/// <summary>
	/// 落下開始
	/// </summary>
	/// <param name="_callback">コールバック</param>
	public void StartFall( System.Action _callback = null ) {
		StartCoroutine( Fall(_callback) );
	}
	
	/// <summary>
	/// 落下処理
	/// </summary>
	/// <param name="_callback">コールバック</param>
	/// <returns></returns>
	IEnumerator Fall(System.Action _callback ) {
		while(gameObject.transform.localPosition.y > 0){
			gameObject.transform.localPosition += Vector3.down * speed * Time.deltaTime;
			yield return 0;
		}
		if ( _callback != null ) {
			_callback();
		}
		yield return true;
	}
}
