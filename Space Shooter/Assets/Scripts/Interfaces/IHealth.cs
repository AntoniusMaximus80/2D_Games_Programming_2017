namespace SpaceShooter
{
    public interface IHealth
    {
        // This method is used for setting the starting health.
        int StartingHealth { get; }
        // This method is used for getting the maximum health.
        int MaximumHealth { get; }
        // This method is used for getting the minimum health.
        int MinimumHealth { get; }
        // This method is used for getting and setting the current amount of health.
        int CurrentHealth { get; set; }
        // This method increases the current health. It takes the health amount change as a parameter.
        void IncreaseHealth(int changeAmount);
        // This method decreases the current health. It takes the health amount change as a parameter.
        void DecreaseHealth(int changeAmount);
    }
}