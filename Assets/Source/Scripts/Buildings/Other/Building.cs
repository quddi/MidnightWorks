using UnityEngine;

namespace Buildings
{
    public class Building : MonoBehaviour
    {
        [field: SerializeField] public Transform StayPoint { get; private set; }
    }
}