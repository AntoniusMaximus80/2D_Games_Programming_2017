using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

namespace SpaceShooter
{
    public class PlayerSpaceship : SpaceshipBase, IHealReceiver
    {
        public override Type UnitType
        {
            get
            {
                return Type.Player;
            }
        }

        public const string HorizontalAxisName = "Horizontal";
        public const string VerticalAxisName = "Vertical";

        [SerializeField]
        private Text _currentHealthText;

        private Health health;

        #region Assignment 2
        public bool _isImmortal = false;

        [SerializeField]
        private int _livesLeft;

        [SerializeField]
        private Text _livesLeftText;

        [SerializeField]
        private float _immortalityDuration;

        private float _immortalityCountdown;
        #endregion

        #region Assignment 3
        [SerializeField]
        private Text _weaponPowerUpText;

        private void UpdateWeaponPowerUpText()
        {
            int absoluteValue = (int)_weaponPowerUpCountdown;
            _weaponPowerUpText.text = absoluteValue.ToString();
        }

        [SerializeField]
        private float _weaponPowerUpDuration;

        private float _weaponPowerUpCountdown;

        private bool _weaponPowerUpActive = false;

        [SerializeField]
        private GameObject _powerUpWeapon;
        #endregion

        private const int _blinkInterval = 8;

        private int _blinkIntervalCountdown;

        Color transparent = new Color(1f, 1f, 1f, 0f);
        Color visible = new Color(1f, 1f, 1f, 1f);

        private void PlayerDeath()
        {
            Debug.Log("PlayerDeath() called.");
            _isImmortal = true;
            _livesLeft--;
            UpdateLivesLeftText();

            if (_livesLeft == 0)
            {
                Destroy(gameObject);
            }
            else
            {
                // Reset player's health back to the starting value.
                health.IncreaseHealth(100);

                // Reset the player's ship's location to the starting location.
                gameObject.transform.position = new Vector2(0f, -4f);
            }
        }

        private void Start()
        {
            health = gameObject.GetComponent<Health>();
            UpdateCurrentHealthText();
            UpdateLivesLeftText();
            _immortalityCountdown = _immortalityDuration;
            _blinkIntervalCountdown = _blinkInterval;
            _powerUpWeapon.SetActive(false);
        }

        private void UpdateCurrentHealthText()
        {
            _currentHealthText.text = "" + health.CurrentHealth.ToString();
        }

        private void UpdateLivesLeftText()
        {
            _livesLeftText.text = _livesLeft.ToString();
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

            // Update the UI text.
            UpdateCurrentHealthText();

            if (_isImmortal)
            {
                // Mechanics for blinking the player spaceship after death.
                _blinkIntervalCountdown--;
                if (_blinkIntervalCountdown == 0)
                {
                    _blinkIntervalCountdown = _blinkInterval;
                    if (gameObject.GetComponent<SpriteRenderer>().color.a > 0f)
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = transparent;
                    }
                    else
                    {
                        gameObject.GetComponent<SpriteRenderer>().color = visible;
                    }
                }

                // Mechanics for counting down the immortality time.
                _immortalityCountdown -= Time.deltaTime;
                if (_immortalityCountdown <= 0f)
                {
                    _immortalityCountdown = _immortalityDuration;
                    _isImmortal = false;
                    gameObject.GetComponent<SpriteRenderer>().color = visible;
                }
            }

            if (_weaponPowerUpActive)
            {
                _weaponPowerUpCountdown -= Time.deltaTime;
                UpdateWeaponPowerUpText();
                if (_weaponPowerUpCountdown <= 0f)
                {
                    _weaponPowerUpActive = false;
                    _powerUpWeapon.SetActive(false);
                    Color transparentTextColor = new Color(255, 185, 0, 0);
                    _weaponPowerUpText.color = transparentTextColor;
                }
            }
        }

        public void ActivateWeaponPowerUp()
        {
            _weaponPowerUpActive = true;
            _weaponPowerUpCountdown = _weaponPowerUpDuration;
            _powerUpWeapon.SetActive(true);

            // Update the weapon power up text and make it visible.
            UpdateWeaponPowerUpText();
            Color visibleTextColor = new Color(255, 185, 0, 255);
            _weaponPowerUpText.color = visibleTextColor;
        }

        protected override void Die()
        {
            if (Health.IsDead)
            {
                PlayerDeath();
            }
        }

        public void ReceiveHeal(int healAmount)
        {
            Health.IncreaseHealth(healAmount);
        }
    }
}
