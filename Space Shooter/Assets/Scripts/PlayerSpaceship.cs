using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace SpaceShooter
{
    public class PlayerSpaceship : SpaceshipBase, IHealth
    {
        public const string HorizontalAxisName = "Horizontal";
        public const string VerticalAxisName = "Vertical";

        // These three fields should be changeable from the Unity editor.
        [SerializeField]
        private int _startingHealth,
            _maximumHealth,
            _minimumHealth;
        // This field can be private, no other class needs to access it.
        private int _currentHealth;

        public Text _currentHealthText;

        private int _framesToOneDamage = 5;

        public int StartingHealth
        {
            get
            {
                return _startingHealth;
            }
        }

        public int MaximumHealth
        {
            get
            {
                return _maximumHealth;
            }
        }

        public int MinimumHealth
        {
            get
            {
                return _minimumHealth;
            }
        }

        public int CurrentHealth
        {
            get
            {
                return _currentHealth;
            }

            set
            {
                _currentHealth = value;
            }
        }

        public void IncreaseHealth(int changeAmount)
        {
            CurrentHealth += changeAmount;
            // Prevent the current health from going over the maximum health.
            if (CurrentHealth > MaximumHealth)
            {
                Debug.Log("Maximum health reached.");
                CurrentHealth = MaximumHealth;
            }
        }

        public void DecreaseHealth(int changeAmount)
        {
            CurrentHealth -= changeAmount;
            // Prevent the current health from going under the minimum health.
            if (CurrentHealth < MinimumHealth)
            {
                Debug.Log("Minimum health reached.");
                CurrentHealth = MinimumHealth;
            }
        }

        private void Start()
        {
            CurrentHealth = StartingHealth;
        }

        private void UpdateCurrentHealthText()
        {
            _currentHealthText.text = "" + CurrentHealth.ToString();
        }

        protected override void Move()
        {
            Vector3 inputVector = new Vector3(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));

            transform.Translate(-inputVector * Speed * Time.deltaTime);
        }

        protected override void Update()
        {
            base.Update();

            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }

            // Decrease health every 10 frames.
            _framesToOneDamage--;
            if (_framesToOneDamage == 0) {
                _framesToOneDamage = 5;
                DecreaseHealth(1);
            }

            // Try to set the health 300 if the health is 0.
            if (CurrentHealth == 0)
            {
                // Adding 300 to see if the IncreaseHealth method works as intended.
                IncreaseHealth(300);
            }

            // Update the UI text.
            UpdateCurrentHealthText();
        }
    }
}
