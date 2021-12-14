namespace AoC6
{
    public class Fish
    {
        private int _InternalTimer;

        public Fish (int internalTimer) =>
            _InternalTimer = internalTimer;

        public double InternalTimer => _InternalTimer;

        public Fish AddDay ()
        {
            _InternalTimer--;
            if (_InternalTimer >= 0)
            {
                return null;
            }
            _InternalTimer = 6;
            return new Fish (8);
        }
    }
}