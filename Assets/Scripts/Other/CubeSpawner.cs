using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CubeSpawner : ITickable 
{
	private GameObjectFactory _factory;

	public CubeSpawner(GameObjectFactory factory) {
		_factory = factory;
	}

	public void Tick() {
		_factory.Create(0);
		_factory.Create(1);
		_factory.Create(2);
	}
}
