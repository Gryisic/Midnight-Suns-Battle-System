using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Utils;

namespace Units
{
    public class UnitsHandler : IDisposable
    {
        public List<Unit> Units { get; } = new List<Unit>();
        public List<Hero> Heroes { get; } = new List<Hero>();
        public List<Enemy> Enemies { get; } = new List<Enemy>();

        private int _currentHeroIndex;

        public void Dispose()
        {
            Heroes.ForEach(u => u.Dispose());
        }
        
        public void Add(Unit unit)
        {
            if (unit == null)
                throw new NullReferenceException("Trying to add null unit");
            
            if (Units.Contains(unit))
                Debug.LogWarning($"Trying to add {unit} that already added in list");
            
            Units.Add(unit);
            
            switch (unit)
            {
                case Hero hero:
                    Heroes.Add(hero);
                    break;
                
                case Enemy enemy:
                    Enemies.Add(enemy);
                    break;
            }
        }

        public bool Remove(Unit unit)
        {
            if (unit == null)
                throw new NullReferenceException("Trying to remove null unit");

            return Units.Remove(unit);
        }

        public Hero GetNextHero()
        {
            _currentHeroIndex = _currentHeroIndex + 1 >= Heroes.Count ? 0 : _currentHeroIndex + 1;

            return Heroes[_currentHeroIndex];
        }

        public Hero GetHeroByType(Enums.UnitType type)
        {
            Hero hero = Heroes.First(h => h.Type == type);

            if (hero == null)
                throw new InvalidOperationException($"Hero of type '{type}' doesn't provided any card");
            
            return hero;
        }
    }
}