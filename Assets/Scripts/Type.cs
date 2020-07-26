using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type : MonoBehaviour
{
	public enum SquareStatus {
		ON_INIT,
		FIRST_CLICK,
		CLICKABLE,
		FLAG,
		CLICKED,
		EXPLOSION
	}

	public enum SquareType {
		INIT,
		NORMAL,	//安全マス
		NUMBER, //数字マス
		BOMB,	//爆弾マス
		EMPTY,  //クリック済みの空のマス
		NONE
	}

}
