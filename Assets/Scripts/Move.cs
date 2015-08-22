using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Move : MonoBehaviour {
	
	/// <summary>
	/// 移動先ポイント
	/// </summary>
	List<Transform> points = new List<Transform>();

	/// <summary>
	/// 現在進行中の移動先
	/// </summary>
	int current;

	/// <summary>
	/// ナビメッシュを動かすコンポーネント
	/// </summary>
	[SerializeField]
	NavMeshAgent navmeshAgent;

	void Update() {
		if ( points == null || points.Count == 0 ) return;
		Vector3 pos = points[current].position;

		if ( Vector3.Distance( transform.position, pos ) < 2.0f ) {
			current = ( current < points.Count - 1 ) ? current + 1 : 0;
		}

		navmeshAgent.SetDestination( pos );
	}

	public void AddPoint(Transform point) {
		this.points.Add( point );
	}

	public void OnDisable() {
		navmeshAgent.Stop();
	}
}
