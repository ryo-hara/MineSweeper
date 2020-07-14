using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
	[SerializeField] private GameObject _cube;
	public override void InstallBindings() {
		Container.BindFactory<BaseCube, BaseCube.Factory>()
			.FromComponentInNewPrefab(_cube)
			.WithGameObjectName("Cube")
			.UnderTransformGroup("Cubes");

		Container.Bind<ITickable>().To<CubeSpawner>().AsSingle();
	}
}