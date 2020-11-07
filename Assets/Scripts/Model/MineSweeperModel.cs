using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using ModestTree;
using ModestTree.Util;

public class MineSweeperModel
{

	//960*720
	public const float DRAWING_SIZE_AREA_X = 550.0f;
	public const float DRAWING_SIZE_AREA_Y = 550.0f;

	public const float DRAWING_START_POINT_X = 500.0f;
	public const float DRAWING_START_POINT_Y = 500.0f;

	//720*1028

	public const int MAX_ROW_NUM = 100;
	public const int MAX_COLUMN_NUM = 100;
	public const int MAX_MINE_NUM = 100;

	private int rowNum = 10; // x
	private int columnNum = 10; // y
	private int mineNum = 10;


	public void CreateSquares(System.Action<int, Vector2, float> onCreateCallBack) 
	{
		Vector2 startPoint = GetCreateStartPointWorld() + new Vector2(GetOnceSquareWorldSizeRatio() / 2, -1 * GetOnceSquareWorldSizeRatio() / 2);
		float squareSizeRatio = GetOnceSquareWorldSizeRatio();

		for (int ii = 0; ii < this.columnNum; ii++) {
			for (int jj = 0; jj < this.rowNum; jj++) {
				float x = startPoint.x + jj * squareSizeRatio;
				float y = startPoint.y - ii * squareSizeRatio;
				int id = jj + ii * this.rowNum;
				Debug.Log("a:"+id);
				onCreateCallBack.Invoke(id, new Vector2(x,y), squareSizeRatio);
			}
		}
	}


	//最初に1マスクリックされてから、クリックしていないマスに地雷と通常マスの情報を与える処理
	public void SetSquareStatus(List<Square> list) 
	{
		var randomList = list.OrderBy(i => System.Guid.NewGuid()).ToList();
		randomList.Take(mineNum).ToList().ForEach(obj => {
			obj.setSquareType(Type.SquareType.BOMB);
			});

		randomList.Skip(mineNum).ToList().ForEach( obj =>{
			obj.setSquareType(Type.SquareType.NORMAL);
		});
	}

	public void SetAroundBombNum(List<Square> list) {
		int squareNum = this.columnNum * this.columnNum;
		for (int i = 0; i < squareNum; i++){
			list[i].SetAroundBombNum(getAdjacentBombNum(list, i));
		}
	}

	//指定マス周辺のBomの数を取得
	private int getAdjacentBombNum(List<Square> list, int index) {

		// id - 1 - x, id - x, id - x + 1
		// id - 1, id + 1
		// id + x - 1, id + x , id + x + 1 

		int bombNum = 0;

		bombNum += getSquareBombNum(index - 1 - this.rowNum, index, list);
		bombNum += getSquareBombNum(index - 1 + this.rowNum, index, list);
		bombNum += getSquareBombNum(index - this.rowNum, index, list);

		bombNum += getSquareBombNum(index - 1, index, list);
		bombNum += getSquareBombNum(index + 1, index, list);

		bombNum += getSquareBombNum(index + 1 - this.rowNum, index, list);
		bombNum += getSquareBombNum(index + 1 + this.rowNum, index, list);
		bombNum += getSquareBombNum(index + this.rowNum, index, list);
		Debug.Log("Index: " + index+"  AroundBomb:" + bombNum);
		return bombNum;
	}

	//検索マスがボムを持っている場合は1,無い場合は0を返す
	private int getSquareBombNum(int searchIndex, int baseIndex, List<Square> list) {

		if (isInRange(searchIndex, baseIndex)) {
			if (list[searchIndex].GetSquareType() == Type.SquareType.BOMB)
				return 1;
		}
		return 0;
	}

	//検索先のIndexが自分の隣接マスか確認する処理
	private bool isInRange(int searchIndex, int baseIndex) {
		if (searchIndex < 0) return false;
		if (searchIndex > this.rowNum * this.columnNum - 1) return false;

		//元のマスが左端の場合かつさらに左のマスを見ようとしている場合
		if (baseIndex % this.rowNum == 0 && (searchIndex == baseIndex - 1 || searchIndex == baseIndex + this.rowNum - 1 || searchIndex == baseIndex - this.rowNum - 1))
			return false;

		//元のマスが右端の場合かつさらに右のマスを見ようとしている場合
		if (baseIndex % this.rowNum == this.rowNum - 1 && (searchIndex == baseIndex + 1 || searchIndex == baseIndex + this.rowNum + 1 || searchIndex == baseIndex - this.rowNum + 1))
			return false;

		return true;
	}



	public int GetSquareAdjacentBombNum(List<Square> list ) {
		return 0;
	}


	private Vector2 GetScreenPixelSize(){ return new Vector2(Screen.width, Screen.height); }

	private Vector2 GetScreenWorldSize(){ return Camera.main.ViewportToWorldPoint(new Vector2(1, 1)) * 2;}

	//一個あたりのScale比率の取得
	private float GetOnceSquareWorldSizeRatio()
	{
		float worldSquareDrawAreaSize = (DRAWING_SIZE_AREA_X / GetScreenPixelSize().x) * GetScreenWorldSize().x;
		return worldSquareDrawAreaSize/ rowNum;
	}

	private Vector2 GetCreateStartPointWorld()
	{
		float adjustedValueX = 0.0f;//
		float adjustedValueY =  -1 * 149.0f / GetScreenPixelSize().y * GetScreenWorldSize().y;//144

		float x = -1 * ((DRAWING_SIZE_AREA_X/2) / GetScreenPixelSize().x ) * GetScreenWorldSize().x;
		float y = ((DRAWING_SIZE_AREA_Y / 2) / GetScreenPixelSize().y) * GetScreenWorldSize().y;
		//ここでのx,yは敷き詰めるオブジェクトの上左端を示す

		return new Vector2(x + adjustedValueX, y + adjustedValueY);
	}

}
