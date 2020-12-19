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

	[SerializeField]
	private GameObject gameClearView = null;

	[SerializeField]
	private GameClearUI gameClearUI = null;

	[SerializeField]
	private MainGameUIView mainGameUI = null;

	[SerializeField]
	private Timer timer = null;


	[Inject]
	private Square.Factory squareFactory = null;
	private MineSweeperModel mineSweeperModel = new MineSweeperModel();

	private List<Square> squareList = new List<Square>();

	private SaveData saveDataObject = null;


	private void Awake() 
	{
		Initialize();
	}

	public void Initialize() 
	{

		saveDataObject = GameObject.Find("SaveData").GetComponent<SaveData>();
		if (saveDataObject != null){
			mineSweeperModel.setData(saveDataObject.squareOneSideNum, saveDataObject.bombNum);
		}

		gameOverView.SetActive(false);
		gameClearView.SetActive(false);
		mineSweeperModel.CreateSquares((index, point , sizeRatio) => {
			//マス内で自分がどこにいてどこに配置されるかサイズと位置を取得する
			var obj = squareFactory.Create(point, sizeRatio);
			obj.index = index;

			obj.onClickedAction = this.SquareOnClickedAction;

			squareList.Add(obj);
		});

		Debug.Log("id__"+string.Join(",", squareList.Select(i => i.index)));

		this.gameOverUI.clickContinueButton.Subscribe(x => this.continueGame());
		this.gameOverUI.clickExitButton.Subscribe(x => this.exitGame());

		this.gameClearUI.clickContinueButton.Subscribe(x => this.continueGame());
		this.gameClearUI.clickExitButton.Subscribe(x => this.exitGame());

		this.mainGameUI.SetBombNum(mineSweeperModel.GetBombNum());

		Observable.Merge(squareList.Select( obj => (obj.squareStatus))).Subscribe( status => {
			switch (status) {
				case Type.SquareStatus.FIRST_CLICK:
					Debug.Log("FIRSTCLICK  全てのオブジェクトに対して行動する");
					//ここでクリックしたオブジェクト以外にマインをセットする
					Debug.Log("残りのマス" + squareList.Where(obj => obj.squareStatus.Value != Type.SquareStatus.FIRST_CLICK).Count());
					mineSweeperModel.SetSquareStatus(squareList.Where(obj => obj.squareStatus.Value != Type.SquareStatus.FIRST_CLICK).ToList());
					Debug.Log("id__" + string.Join(",", squareList.Select(i => i.index)));

					mineSweeperModel.SetAroundBombNum(squareList);
					this.FirstClickedActionEnded();
					break;

				case Type.SquareStatus.CLICKED:
					int cleckedSquareNum = squareList.Count(square => (square.GetSquareType() == Type.SquareType.NORMAL) && square.squareStatus.Value == Type.SquareStatus.CLICKED);
					Debug.Log("Clecked: " + cleckedSquareNum);
					//初回クリックの値を足す
					if (cleckedSquareNum + 1 >= mineSweeperModel.GetSquareNum() - mineSweeperModel.GetBombNum())
						gameClear();	
					break;

				case Type.SquareStatus.EXPLOSION:
					this.gameOver();
					break;
			}

			timer.countTimeBehaviorSubject.Subscribe(time => mainGameUI.SetTime(time));

			int fragNum = squareList.Count(obj => obj.IsFlagActive());
			this.mainGameUI.SetFragNum(fragNum);

		}).AddTo(this);
	}

	private void SquareOnClickedAction(int index){
		mineSweeperModel.checkNonAroundBombSquare(squareList, index);
	}

	private void FirstClickedActionEnded(){
		foreach(var obj in squareList){
			obj.FirstClickAction();
		}
		timer.TimerStart();
	}


	private void gameOver() {
		Debug.Log("GameOver");

		timer.TimerStop();
		gameOverView.SetActive(true);
		foreach (var obj in squareList) {
			obj.isClickable = false;
		}
	}
	private void gameClear() {
		Debug.Log("GameClear");

		timer.TimerStop();
		gameClearView.SetActive(true);
		foreach (var obj in squareList) {
			obj.isClickable = false;
		}
	}


	private void continueGame(){
		Debug.Log("Continue");

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
