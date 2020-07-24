using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeperModel
{

	public const int MAX_ROW_NUM = 100;
	public const int MAX_COLUMN_NUM = 100;
	public const int MAX_MINE_NUM = 100;

	private int rowNum = 4;
	private int columnNum = 4;
	private int mineNum = 1;


	public void CreateSquare(System.Action onCreateCallBack) {
		for (int ii = 0; ii < this.columnNum; ii++) {
			for (int jj = 0; jj < this.rowNum; jj++) {
				onCreateCallBack.Invoke();
			}
		}
	}


}
