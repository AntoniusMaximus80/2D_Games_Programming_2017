using UnityEngine;

namespace SpaceShooter
{
    public abstract class PowerUpBase : MonoBehaviour
    {
        public enum Type
        {
            Health = 0,
            Weapon = 1
        }
    }
}