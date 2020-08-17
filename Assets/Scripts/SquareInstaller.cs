using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


public class SquareInstaller : MonoInstaller
{
	[SerializeField] private Square prefab;

	public override void InstallBindings() 
	{
		Container.Bind<Square.Factory>().AsSingle().WithArguments(prefab);
	}
}
