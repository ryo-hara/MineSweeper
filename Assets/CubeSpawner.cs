using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CubeSpawner : ITickable {
	private BaseCube.Factory _factory;

	public CubeSpawner(BaseCube.Factory factory) {
		_factory = factory;
	}

	public void Tick() {
		_factory.Create();
	}
}
