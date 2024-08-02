
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

        public float tileWidth = 0f;
    }

