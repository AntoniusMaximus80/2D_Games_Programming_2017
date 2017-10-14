using System;
using UnityEngine;

namespace SpaceShooter
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int _minHealth;

        [SerializeField]
        private int _maxHealth;

        [SerializeField]
        private int _currentHealth;

        public int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }
        }

        public void DecreaseHealth(int amount)
        {
            _currentHealth -= Mathf.Clamp(amount, _minHealth, _maxHealth);
        }

        public void IncreaseHealth(int amount)
        {
            _currentHealth += Mathf.Clamp(amount, _minHealth, _maxHealth);
        }

        public bool IsDead
        {
            get { return CurrentHealth <= _minHealth; }
        }
    }
}