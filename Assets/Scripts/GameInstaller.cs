using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
	[SerializeField] private GameObject[] _cubes;

	public override void InstallBindings() {
		Container.Bind<ITickable>().To<CubeSpawner>().AsSingle();

		Container.Bind<BaseCube.CubeFactory>().AsSingle().WithArguments(_cubes);
	}
}