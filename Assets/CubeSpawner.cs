using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CubeSpawner : ITickable 
{
	private BaseCube.CubeFactory _factory;

	public CubeSpawner(BaseCube.CubeFactory factory) {
		_factory = factory;
	}

	public void Tick() {
		_factory.Create(0);
		_factory.Create(1);
		_factory.Create(2);
	}
}
