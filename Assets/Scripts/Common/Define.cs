
    public class Define
    {
        public enum WorldObject
        {
            Unknown,
            Player,
            Slime,
            Goblin,
        }

        public enum Layer
        {
            Ground = 6,
            Level1,
            LevelN,
            Player = 15,
            Monster = 16,
        }
        public enum State
        {
            Die,
            Moving,
            Jumping,
            Idle,
            Attack,
            Target,
        }

        public enum UIEvent
        {
            Click,
            Drag,
        }
        public enum MouseEvent
        {
            Press,
            PointerDown,
            PointerUp,
            Click,
        }
        
    }

