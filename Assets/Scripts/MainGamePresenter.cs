using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainGamePresenter : MonoBehaviour
{
	[Inject]
	private Square.Factory squareFactory;
	private MineSweeperModel mineSweeperModel = new MineSweeperModel();

	public void Initialize() {
		mineSweeperModel.CreateSquare((point , sizeRatio) => {
		//マス内で自分がどこにいてどこに配置されるかサイズと位置を取得する
		var obj = squareFactory.Create();
		obj.GetComponent<Transform>().position = point;
		obj.GetComponent<Transform>().localScale = new Vector3(sizeRatio, sizeRatio, sizeRatio);
		//生成時に位置を決める。また、Objectの属性も決める

		});
	}

	private void Awake() {
		Initialize();
	}
}
