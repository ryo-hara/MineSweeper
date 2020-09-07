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
		BOMB,	//爆弾マス
		NONE
	}

}
