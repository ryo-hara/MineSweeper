using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MainGameUIView : MonoBehaviour
{

	[SerializeField] TextMeshProUGUI bombNum;
	[SerializeField] TextMeshProUGUI fragNum;
	[SerializeField] TextMeshProUGUI timeText;


	private void Awake() {
		bombNum.text = "0";
		fragNum.text = "0";
		timeText.text = "00:00";
	}



	public void SetBombNum(int num){
		bombNum.text = num.ToString();
	}

	public void SetFragNum(int num) {
		fragNum.text = num.ToString();
	}

	public void SetTime(float time) {
		int allTime = Convert.ToInt32(time);
		int minut = allTime / 60;
		int second = allTime % 60;

		string minutText = minut.ToString();
		string secondText = second.ToString();

		if (minut == 0) minutText = "00";
		if (second == 0) minutText = "00";


		if (minut < 10 && minut > 0) minutText = "0" + minutText;
		if (second < 10 && second > 0) secondText = "0" + secondText;


		timeText.text = minutText + ":" + secondText;
	}

}
