using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameObjectFactory : IFactory<int, GameObject> 
{
	private DiContainer _container;
	private GameObject[] _gameObjects;

	[Inject]
	public void Construct(GameObject[] gameObjects, DiContainer container) {
		_container = container;
		_gameObjects = gameObjects;
	}

	public GameObject Create(int i) {
		return _container.InstantiatePrefab(_gameObjects[i]);
	}
}