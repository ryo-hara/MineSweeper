using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
	public int bombNum = 0;
	public int squareOneSideNum = 0;

	void Start() {
		DontDestroyOnLoad(this);
	}

}
