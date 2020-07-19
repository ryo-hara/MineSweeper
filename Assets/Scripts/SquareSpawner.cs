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
		mineSweeperModel.CreateSquare(() => { squareFactory.Create(); });
	}
}
