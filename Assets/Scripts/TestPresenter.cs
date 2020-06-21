using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using UniRx;
using UnityEngine.EventSystems;


public class TestPresenter : MonoBehaviour
{

	public GameObject minesObj;

	void Start()
    {
		var mines = minesObj.GetComponentsInChildren<Square>();

		var observable = Observable.Merge(mines.Select(x => x.squareStatus.AsObservable()));


		observable.Subscribe(x => {
			if ( x == Type.SquareStatus.CLICKED ) {
				Debug.Log("is CLICKED");
			}
			if (x == Type.SquareStatus.EXPLOSION) {
				Debug.Log("is EXPLOSION");
			}
			if (x == Type.SquareStatus.CLICKABLE) {
				Debug.Log("is CLICKABLE");
			}
			if (x == Type.SquareStatus.FLAG) {
				Debug.Log("is CLICKABLE");
			}


		});


	}

}
