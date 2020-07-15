using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameObjectFactory : IFactory<GameObject> {
	DiContainer _container;
	UnityEngine.Object _prefab;

	[Inject]
	public void Construct(
		UnityEngine.Object prefab,
		DiContainer container) {
		_container = container;
		_prefab = prefab;
	}

	public GameObject Create() {
		return _container.InstantiatePrefab(_prefab);
	}
}