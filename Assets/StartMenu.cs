using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UniRx;
using UnityEngine.SceneManagement;



public class StartMenu : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI bombNumText;
    [SerializeField] TextMeshProUGUI squareNumText;
    [SerializeField] TextMeshProUGUI squareDetailNumText;

    [SerializeField] Slider bombSlider;
    [SerializeField] Slider squareSlider;

    [SerializeField] private StandardButton startButton;

    SaveData saveDataObject = null;

    private int bombNum = 0;
    private int squareNum = 0;

	private void Awake() {

        saveDataObject = GameObject.Find("SaveData").GetComponent<SaveData>();

        bombSlider.OnValueChangedAsObservable().Subscribe(num => {
            this.bombNum = (int)num;
            this.bombNumText.text = this.bombNum.ToString();
            if (saveDataObject != null) saveDataObject.bombNum = this.bombNum;
        });

        squareSlider.OnValueChangedAsObservable().Subscribe(num => {
            this.squareNum = (int)num * (int)num;
            this.squareDetailNumText.text = (int)num + "*" + (int)num;
            this.squareNumText.text = this.squareNum.ToString();
            if (saveDataObject != null) saveDataObject.squareOneSideNum = (int)num;
        });




        startButton.SetButtonAction(this.startGame);
    }


	private void Start() {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void startGame(){
        SceneManager.LoadScene("SampleScene");
    }

}
