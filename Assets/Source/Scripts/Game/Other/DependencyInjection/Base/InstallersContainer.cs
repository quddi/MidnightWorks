using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using VContainer.Unity;

namespace Game
{
    public class InstallersContainer : SerializedMonoBehaviour
    {
        [field: SerializeField] public List<IInstaller> InjectableServicesInstallers { get; private set; } = new();
        [field: SerializeField] public List<IInstaller> DefaultServicesInstallers { get; private set; } = new();
    }
}