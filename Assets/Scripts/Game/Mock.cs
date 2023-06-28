using Units;
using UnityEngine;

namespace Game
{
    public class Mock : MonoBehaviour
    {
        [SerializeField] private Hero _playerUnit;
        [SerializeField] private Unit[] _units;

        public Hero PlayerUnit => _playerUnit;
        public Unit[] Units => _units;
    }
}