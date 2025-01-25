namespace Game.Model
{
    public abstract class PowerUp
    {
        public float Volume;
        public float Mass;
        
        public PowerUp(float volume, float mass)
        {
            Volume = volume;
            Mass = mass;
        }
    }
}