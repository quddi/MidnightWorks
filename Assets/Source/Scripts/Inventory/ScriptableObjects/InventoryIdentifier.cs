using System.Linq;
using Extensions;
using UnityEngine;

namespace Inventory
{
    [CreateAssetMenu(fileName = "Inventory Identifier", menuName = "ScriptableObjects/Inventory/Inventory identifier")]
    public class InventoryIdentifier : ScriptableObject
    {
        [SerializeField] private string _id;

        public string Id => _id;

        public override int GetHashCode()
        {
            return Id == null ? default : Id.GetHashCode();
        }

        public override bool Equals(object other)
        {
            return other is InventoryIdentifier inventoryIdentifier && inventoryIdentifier.Id == Id;
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            var otherIdentifiers = ExtensionMethods.GetAllScriptableObjects<InventoryIdentifier>().ToList();

            otherIdentifiers.Remove(this);

            var sameIdIdentifiers = otherIdentifiers.Where(identifier => identifier.Id == Id).ToList();
            
            if (!sameIdIdentifiers.Any())
                return;
            
            foreach (var sameIdIdentifier in sameIdIdentifiers)
            {
                Debug.LogError($"Found an inventory identifier as this ({name}). Asset's name: [{sameIdIdentifier.name}]");
            }
        }
#endif
    }
}