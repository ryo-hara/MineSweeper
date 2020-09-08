﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System.Linq;

public class MainGamePresenter : MonoBehaviour
{
	[Inject]
	private Square.Factory squareFactory = null;
	private MineSweeperModel mineSweeperModel = new MineSweeperModel();

	private List<Square> squareList = new List<Square>();



	private void Awake() 
	{
		Initialize();
	}

	public void Initialize() 
	{
		mineSweeperModel.CreateSquares((point , sizeRatio) => {
		//マス内で自分がどこにいてどこに配置されるかサイズと位置を取得する
		var obj = squareFactory.Create(point, sizeRatio);
		squareList.Add(obj);
		});


		Observable.Merge(squareList.Select( obj => obj.squareStatus)).Subscribe( status => {
			switch (status) {
				case Type.SquareStatus.FIRST_CLICK:
					Debug.Log("FIRSTCLICK  全てのオブジェクトに対して行動する");
					//ここでクリックしたオブジェクト以外にマインをセットする
					Debug.Log("残りのマス" + squareList.Where(obj => obj.squareStatus.Value != Type.SquareStatus.FIRST_CLICK).Count());
					mineSweeperModel.SetSquareStatus(squareList.Where(obj => obj.squareStatus.Value != Type.SquareStatus.FIRST_CLICK).ToList());
					break;

				case Type.SquareStatus.EXPLOSION:
					Debug.Log("GameOver");
					break;

			}
		}).AddTo(this);
	}

}
