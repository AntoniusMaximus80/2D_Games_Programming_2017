namespace SpaceShooter
{
    public interface IHealth
    {
        // This method is used for getting the current amount of health.
        int CurrentHealth { get; }
        bool IsDead { get; }
        // This method increases the current health. It takes the health amount change as a parameter.
        void IncreaseHealth(int amount);
        // This method decreases the current health. It takes the health amount change as a parameter.
        void DecreaseHealth(int amount);
    }
}