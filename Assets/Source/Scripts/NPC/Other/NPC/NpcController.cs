using System.Linq;
using Extensions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NPC
{
    public class NpcController : SerializedMonoBehaviour
    {
        [SerializeField, TabGroup("Components")] private Transform _leavingPoint;
        [SerializeField, TabGroup("Components")] private BoxCollider _promenadingCollider;
        [SerializeField, TabGroup("Components")] private INpcPool _npcPool;
        
#if UNITY_EDITOR
        [Button]
        private void Spawn()
        {
            var configs = ExtensionMethods.GetAllScriptableObjects<NpcConfig>().ToList();
            
            foreach (var npcConfig in configs)
            {
                var npc = _npcPool.DeployNpc(npcConfig);

                npc.PromenadingBoundsCollider = _promenadingCollider;
                npc.LeavingPoint = _leavingPoint;
            }
        }
            
#endif
    }
}