using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineSweeperModel
{

	//960*720
	public const float DRAWING_SIZE_AREA_X = 500.0f;
	public const float DRAWING_SIZE_AREA_Y = 500.0f;

	public const int MAX_ROW_NUM = 100;
	public const int MAX_COLUMN_NUM = 100;
	public const int MAX_MINE_NUM = 100;

	private int rowNum = 10;
	private int columnNum = 10;
	private int mineNum = 1;



	public void CreateSquare(System.Action<Vector2, float> onCreateCallBack) 
	{

		Vector2 startPoint = GetCreateStartPointWorld() + new Vector2(GetOnceSquareWorldSizeRatio() / 2, -1 * GetOnceSquareWorldSizeRatio() / 2);
		float squareSizeRatio = GetOnceSquareWorldSizeRatio();

		for (int ii = 0; ii < this.columnNum; ii++) {
			for (int jj = 0; jj < this.rowNum; jj++) {
				float x = startPoint.x + ii * squareSizeRatio;
				float y = startPoint.y - jj * squareSizeRatio;
				onCreateCallBack.Invoke( new Vector2(x,y), squareSizeRatio);
			}
		}
	}

	private void SetColumnSize(){
		
	}

	private Vector2 GetScreenPixelSize(){ return new Vector2(Screen.width, Screen.height); }

	private Vector2 GetScreenWorldSize(){ return Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;}

	private float GetOnceSquareWorldSizeRatio(){//一個あたりのScale比率の取得
		float worldSquareDrawAreaSize = (DRAWING_SIZE_AREA_X / GetScreenPixelSize().x) * GetScreenWorldSize().x;
		return worldSquareDrawAreaSize/ rowNum;
	}

	private Vector2 GetCreateStartPointWorld(){
		float x = -1 * ((DRAWING_SIZE_AREA_X/2) / GetScreenPixelSize().x ) * GetScreenWorldSize().x;
		float y = ((DRAWING_SIZE_AREA_Y / 2) / GetScreenPixelSize().y) * GetScreenWorldSize().y;
		//ここでのx,yは敷き詰めるオブジェクトの上左端を示す
		return new Vector2(x, y);
	}

}
