using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System.Linq;

public class MainGamePresenter : MonoBehaviour
{
	[Inject]
	private Square.Factory squareFactory;
	private MineSweeperModel mineSweeperModel = new MineSweeperModel();

	private List<Square> squareList = new List<Square>();

	public void Initialize() {
		mineSweeperModel.CreateSquare((point , sizeRatio) => {
		//マス内で自分がどこにいてどこに配置されるかサイズと位置を取得する
		var obj = squareFactory.Create();

		obj.GetComponent<Transform>().position = point;
		obj.GetComponent<Transform>().localScale = new Vector3(sizeRatio, sizeRatio, sizeRatio);

		squareList.Add(obj);
		});


		Observable.Merge(squareList.Select( x => x.squareStatus)).Subscribe( status => {
			switch (status) {
				case Type.SquareStatus.FIRST_CLICK:
					Debug.Log("FIRSTCLICK  全てのオブジェクトに対して行動する");
					break;
			}
		});


	}

	private void Awake() {
		Initialize();
	}
}
