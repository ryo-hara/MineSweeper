﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class SquareInstaller : MonoInstaller
{
	[SerializeField] private GameObject prefab;

	public override void InstallBindings() {
		Container.Bind<IInitializable>().To<SquareSpawner>().AsSingle();
		Container.BindFactory<Square, Square.Factory>().FromComponentInNewPrefab(prefab);
	}
}
