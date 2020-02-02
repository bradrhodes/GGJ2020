using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class TowerInstaller : Installer
{
    [Inject]
    public TowerParameters Parameters { private get; set; }

    public override void InstallBindings()
    {
        Container.BindInstance(Parameters);
        Container.BindInstance(this);
    }
}
