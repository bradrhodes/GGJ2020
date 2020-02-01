using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MapInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        Container.Bind<MapAggregate>().AsSingle();
        Container.Bind<MapGenerator>().AsTransient();
    }
}
