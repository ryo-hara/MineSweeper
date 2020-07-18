using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class SquareSpawner : IInitializable
{
	[Inject]
	private Square.Factory squareFactory;

	private MineSweeperModel mineSweeperModel = new MineSweeperModel();

	public void Initialize() {

		for (int ii = 0; ii < mineSweeperModel.columnNum; ii++) {
			for (int jj = 0; jj < mineSweeperModel.rowNum; jj++) {
				squareFactory.Create();
			}
		}
	}
}
