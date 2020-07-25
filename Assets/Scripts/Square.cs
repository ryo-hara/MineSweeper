using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using Zenject;


public class Square : MonoBehaviour 
{
	[SerializeField]
	private StandardButton standardButton;

	public BehaviorSubject<Type.SquareStatus> squareStatus = new BehaviorSubject<Type.SquareStatus>(Type.SquareStatus.ON_INIT);
	private Type.SquareType squareType = Type.SquareType.NONE;

	private void Awake() 
	{
		squareStatus.OnNext(Type.SquareStatus.ON_INIT);
		standardButton.SetButtonAction(this.onClick);
	}

	public void setSquareType( Type.SquareType type)
	{
		this.squareType = type;
	}

	public void onClick()
	{
		switch(squareType){

			case Type.SquareType.NONE:
				Debug.Log("FIRST_CLICK");
				squareStatus.OnNext(Type.SquareStatus.FIRST_CLICK);
				break;

			case Type.SquareType.BOMB:
				Debug.Log("BOMB");
				squareStatus.OnNext(Type.SquareStatus.EXPLOSION);
				break;

			case Type.SquareType.EMPTY:
				Debug.Log("EMPTY");
				squareStatus.OnNext(Type.SquareStatus.CLICKED);
				break;
			
			default:
				Debug.Log("DEFAULT");
				squareStatus.OnNext(Type.SquareStatus.CLICKED);
				break;
		}
	}


	public class Factory : PlaceholderFactory<Square> { }

}
