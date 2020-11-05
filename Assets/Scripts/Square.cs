﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using UnityEngine.EventSystems;
using Zenject;


public class Square : MonoBehaviour 
{
	[SerializeField]
	private StandardButton standardButton;
	[SerializeField]
	private GameObject fragIconObject;
	[SerializeField]
	private GameObject bombIconObject;
	[SerializeField]
	private GameObject clickedSquareObject;

	public int index = 0;

	public BehaviorSubject<Type.SquareStatus> squareStatus = new BehaviorSubject<Type.SquareStatus>(Type.SquareStatus.ON_INIT);
	private Type.SquareType squareType = Type.SquareType.INIT;

	public bool isClickable = true;

	private int aroundBombNum = 0;


	private void Awake() 
	{
		squareStatus.OnNext(Type.SquareStatus.ON_INIT);
		standardButton.SetButtonAction(this.onClick);
		fragIconObject.SetActive(false);
		bombIconObject.SetActive(false);
		clickedSquareObject.SetActive(false);
	}

	public void setSquareType(Type.SquareType type)
	{
		this.squareType = type;
		switch (squareType) {
		//	case Type.SquareType.NORMAL:
			//	fragIconObject.SetActive(true);
			//break;

			case Type.SquareType.BOMB:
				bombIconObject.SetActive(true);
				break;
		}
	}

	public Type.SquareType GetSquareType()
	{
		return this.squareType;
	}

	public void SetAroundBombNum(int bombNum){
		this.aroundBombNum = bombNum;
	}

	public void onClick()
	{
		if (!isClickable) return; 
		switch(squareType){

			case Type.SquareType.INIT:
				Debug.Log("FIRST_CLICK:" + index);
				squareStatus.OnNext(Type.SquareStatus.FIRST_CLICK);
				squareType = Type.SquareType.NONE;
				clickedSquareObject.SetActive(true);
				this.isClickable = false;
				break;

			case Type.SquareType.NORMAL:
				Debug.Log("NORMAL:"+ index);
				squareStatus.OnNext(Type.SquareStatus.CLICKED);
				clickedSquareObject.SetActive(true);
				this.isClickable = false;
				Debug.Log("AroundBomb:"+ aroundBombNum);
				break;

			case Type.SquareType.BOMB:
				Debug.Log("BOMB:" + index);
				squareStatus.OnNext(Type.SquareStatus.EXPLOSION);
				break;

			default:
				Debug.Log("DEFAULT:" + index);
				squareStatus.OnNext(Type.SquareStatus.CLICKED);
				break;
		}
	}

	public void onDestroy() 
	{
		Destroy(this.gameObject);	
	}

	public class Factory : IFactory<Vector2, float, Square> 
	{
		private DiContainer _container;
		private Square _gameObject;

		[Inject]
		public void Construct(Square gameObject, DiContainer container) 
		{
			_container = container;
			_gameObject = gameObject;
		}

		public Square Create(Vector2 point, float sizeRatio) 
		{
			var square = _container.InstantiatePrefabForComponent<Square>(_gameObject);
			square.transform.position = point;
			square.transform.localScale = new Vector3(sizeRatio, sizeRatio, sizeRatio);

			return square;
		}
	}

}
