using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace UI
{
    [CreateAssetMenu(fileName = "Ui Service Config", menuName = "ScriptableObjects/Ui/Ui service config")]
    public class UiServiceConfig : SerializedScriptableObject
    {
        [field: SerializeField] public Dictionary<Type, Window> WindowsPrefabs { get; private set; } = new();
    }
}