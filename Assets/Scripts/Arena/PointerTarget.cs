using UnityEngine;
using Utils;

namespace Arena
{
    public class PointerTarget : MonoBehaviour
    {
        [SerializeField] private Enums.PointerTargetType _pointerTargetType;

        public Enums.PointerTargetType PointerTargetType => _pointerTargetType;
    }
}