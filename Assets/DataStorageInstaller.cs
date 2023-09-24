using UnityEngine;
using Zenject;

[RequireComponent(typeof(DataStorage))]
public class StorageInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<DataStorage>()
            .FromInstance(GetComponent<DataStorage>())
            .AsSingle();
    }
}
