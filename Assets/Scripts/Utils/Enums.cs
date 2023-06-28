namespace Utils
{
    public static class Enums
    {
        public enum TargetType
        {
            OppositeToUnit,
            SameAsUnit
        }
        
        public enum TargetSelectionType
        {
            Single,
            Chain,
            Area,
            Radius
        }
        
        public enum SkillType
        {
            Heroic,
            Attack,
            Skill
        }

        public enum UnitType
        {
            HeroMelee,
            HeroKick,
            HeroMage,
            EnemyRange
        }
        
        public enum MouseState
        {
            Free,
            Move,
            Selection,
            Selected,
        }

        public enum StandardAnimationType
        {
            Idle, 
            Run,
            TakeDamage
        }

        public enum PointerTargetType
        {
            Unit,
            Confiner,
            Floor,
            Interactable,
            UI
        }
        
        public enum SkillAffectType
        {
            DealDamage,
            Heal,
        }
        
        public enum NavigationLineType
        {
            Dash,
            Strict,
            None
        }
        
        public enum ActiveUIType
        {
            UnitData,
            Rule
        }

        public enum UnitDataType
        {
            Full,
            HealthOnly
        }
        
        public enum RuleType
        {
            CardPlays,
            Redraws,
            Moves,
            Heroism,
        }
    }
}