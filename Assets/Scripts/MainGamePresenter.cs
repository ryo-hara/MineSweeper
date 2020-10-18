using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using UniRx;
using System.Linq;
using UnityEngine.SceneManagement;


public class MainGamePresenter : MonoBehaviour
{
	[SerializeField]
	private GameObject gameOverView = null;

	[SerializeField]
	private GameOverUI gameOverUI = null;


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
		gameOverView.SetActive(false);
		mineSweeperModel.CreateSquares((point , sizeRatio) => {
		//マス内で自分がどこにいてどこに配置されるかサイズと位置を取得する
		var obj = squareFactory.Create(point, sizeRatio);
		squareList.Add(obj);
		});

		this.gameOverUI.clickContinueButton.Subscribe(x => this.continueGame());

		this.gameOverUI.clickExitButton.Subscribe(x => this.exitGame());


		Observable.Merge(squareList.Select( obj => obj.squareStatus)).Subscribe( status => {
			switch (status) {
				case Type.SquareStatus.FIRST_CLICK:
					Debug.Log("FIRSTCLICK  全てのオブジェクトに対して行動する");
					//ここでクリックしたオブジェクト以外にマインをセットする
					Debug.Log("残りのマス" + squareList.Where(obj => obj.squareStatus.Value != Type.SquareStatus.FIRST_CLICK).Count());
					mineSweeperModel.SetSquareStatus(squareList.Where(obj => obj.squareStatus.Value != Type.SquareStatus.FIRST_CLICK).ToList());
					break;

				case Type.SquareStatus.EXPLOSION:
					this.gameOver();
					break;

			}
		}).AddTo(this);
	}

	private void gameOver() {
		Debug.Log("GameOver");

		gameOverView.SetActive(true);
		foreach (var obj in squareList) {
			obj.isClickable = false;
		}
	}

	private void continueGame(){
		Debug.Log("Cpntinue");

		Scene loadScene = SceneManager.GetActiveScene();
		SceneManager.LoadScene(loadScene.name);

	}

	private void exitGame() {
		Debug.Log("Exit");
		#if UNITY_EDITOR	
			UnityEditor.EditorApplication.isPlaying = false;
		#elif UNITY_STANDALONE
			UnityEngine.Application.Quit();
		#endif
	}


}
