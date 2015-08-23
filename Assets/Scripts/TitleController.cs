using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;

public class TitleController : MonoBehaviour {

	[SerializeField] private bool _isServer ;
	[SerializeField]
	private Text _score;

	// Use this for initialization
	void Start () {

	#if !UNITY_EDITOR && UNITY_ANDROID
		_isServer = false ;
	#else
		_isServer = true ;
	#endif

		var datas = new List<List<PlayData>>();
		var cntKey = "KEY_COUNT";
		var savecnt = PlayerPrefs.GetInt( cntKey, 0 );
		for ( int i = 0; i < savecnt; i++ ) {
			var json = PlayerPrefs.GetString( i.ToString() );
			datas.Add( LitJson.JsonMapper.ToObject<List<PlayData>>( json ) );
		}
		var max  = datas.Max( n => n.Max( m => m.time ) );
		_score.text = string.Format( "ハイスコア : {0} びょう", max.ToString( "#" ) );
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ToMainGame () {
		if (_isServer) {
			Application.LoadLevel( "Main" ) ;
		}
		else {
			Application.LoadLevel( "dev_Player_Ayaki" ) ;
		}
	}
}
