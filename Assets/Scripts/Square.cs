using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

using UnityEngine.EventSystems;


public class Square : MonoBehaviour, IPointerClickHandler {

	public Subject<Type.SquareStatus> squareStatus = new Subject<Type.SquareStatus>();

	private Type.SquareType squareType = Type.SquareType.NONE;

	public void OnPointerClick(PointerEventData eventData) {
		Debug.Log(eventData.pointerEnter.name);
		squareStatus.OnNext(Type.SquareStatus.CLICKED);
	}


	public void setSquareType( Type.SquareType type) {
		this.squareType = type;
	}

	public void onClick(){
		switch(squareType){
			case Type.SquareType.BOMB:
				Debug.Log("BOMB");
				squareStatus.OnNext(Type.SquareStatus.EXPLOSION);
				break;

			case Type.SquareType.EMPTY:
				squareStatus.OnNext(Type.SquareStatus.CLICKED);
				break;
			
			default: 
				break;
		}

	}

    void Start()
    {
		squareStatus.OnNext(Type.SquareStatus.CLICKABLE);
	}
}
