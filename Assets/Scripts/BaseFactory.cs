using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BaseFactory : IFactory<GameObject> 
{
	private DiContainer container;
	private GameObject gameObject;

	[Inject]
	public void Construct(GameObject gameObject, DiContainer container) {
		this.container = container;
		this.gameObject = gameObject;
	}

	public GameObject Create() {
		return container.InstantiatePrefab(gameObject);
	}
}