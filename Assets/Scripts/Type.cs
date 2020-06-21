using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type : MonoBehaviour
{
	public enum SquareStatus {
		CLICKABLE,
		FLAG,
		CLICKED,
		EXPLOSION
	}

	public enum SquareType {
		BOMB,
		EMPTY,
		NONE
	}

}
