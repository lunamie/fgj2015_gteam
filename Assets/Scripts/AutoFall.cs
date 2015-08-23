using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using LitJson;

public class AutoFall : MonoBehaviour {

	const int TYPE_MOVE = 1;
	const int TYPE_END = 2;

	[System.Serializable]
	public class PlayData {
		public double time;
		public int type;
		public double value;
	}

	[SerializeField]
	List<PlayData> timeLine = new List<PlayData>();


	[SerializeField]
	float _speed = 0.1f;

	[SerializeField]
	float _fGravityAccele = 0.0f;
	int playIndex;


	/// <summary>
	/// 落下速度
	/// </summary>
	public float speed {
		get { return _speed; }
		set { _speed = value; }
	}
	
	/// <summary>
	/// 落下加速度
	/// </summary>
	public float gravityAccele {
		get { return _fGravityAccele; }
		set { _fGravityAccele = value; }
	}
	public void PlayTimeLine( string json, System.Action _callback = null) {

		timeLine = LitJson.JsonMapper.ToObject<List<PlayData>>( json );
		StartCoroutine( EventMove( _callback ) );

	}


	IEnumerator EventMove( System.Action _callback ) {
		float eventTime = 0;
		var moveEvents = timeLine.Where( n => n.type == TYPE_MOVE || n.type == TYPE_END ).Select( ( _event, _index ) => new { _index, _event } ).ToArray();
		while ( true ) {
			
			eventTime += Time.deltaTime;
			var end = moveEvents.FirstOrDefault( n => n._event.time > eventTime);
			if ( end == null ) {
				Debug.Log( "noend" );
				break;
			}
			var start = moveEvents[end._index - 1];
			Debug.Log( "start.index = " + start._event.value  );

			var diffTime = (float)end._event.time - (float)start._event.time;
			var diffValue =(float)end._event.value - (float)start._event.value;

			var diffTimeNow = (float)eventTime - (float)start._event.time;

			var per =diffTimeNow / diffTime;

			Debug.Log( "diffTimeEnd = " + diffTime + ",diffValueEnd = " + diffValue + ",diffTimeNow = " + diffTimeNow + ",per = " + per + ",ypos = " + (float)start._event.value + diffValue * per );

			gameObject.transform.localPosition =
				new Vector3(
					gameObject.transform.localPosition.x,
					(float)( start._event.value + System.Math.Round( diffValue * per, 3 ) ),
					gameObject.transform.localPosition.z );

			yield return 0;
		}
		if ( _callback != null ) {
			_callback();
		}
		yield return true;
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
		float eventTime = 0;
		float prevPosY = gameObject.transform.localPosition.y;
		timeLine.Add( new PlayData() {
			time = System.Math.Round( eventTime, 3 ),
			type = TYPE_END,
			value = prevPosY,
		} );

		while(gameObject.transform.localPosition.y > 0){
			gameObject.transform.localPosition += Vector3.down * (speed + gravityAccele) * Time.deltaTime;

			//	: code by Ayaki on 8/23/10:53.
			gravityAccele += Time.deltaTime * 0.125f ;

			eventTime += Time.deltaTime;
			if ( Mathf.Abs( gameObject.transform.localPosition.y - prevPosY ) > 1 ) {
				prevPosY = gameObject.transform.localPosition.y;
				timeLine.Add( new PlayData() {
					time = System.Math.Round( eventTime, 3 ),
					type = TYPE_MOVE,
					value = System.Math.Round( prevPosY, 3 ),
				} );
			}
			yield return 0;
		}

		timeLine.Add( new PlayData() {
			time = System.Math.Round( eventTime, 3 ),
			type = TYPE_END,
			value = 0,
		} );

		var json = LitJson.JsonMapper.ToJson( timeLine );
		Debug.Log( json );
		if ( _callback != null ) {
			_callback();
		}

		var cntKey = "KEY_COUNT";
		var savecnt = PlayerPrefs.GetInt(cntKey,0);
		PlayerPrefs.SetString( savecnt.ToString(), json );
		PlayerPrefs.SetInt( cntKey, ++savecnt );
		PlayerPrefs.Save();


		yield return true;
	}

}
