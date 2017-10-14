using UnityEngine;
using UnityEngine.UI;
using System.Collections;


namespace SpaceShooter
{
    public class PlayerSpaceship : SpaceshipBase
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

        // ASSIGNMENT 2 START =*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*

        public bool _isImmortal = false;

        [SerializeField]
        private int _livesLeft;

        [SerializeField]
        private Text _livesLeftText;

        [SerializeField]
        private float _immortalityTime;

        private float _immortalityTimeCountdown;

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

        // ASSIGNMENT 2 END =*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*=*

        private void Start()
        {
            health = gameObject.GetComponent<Health>();
            UpdateCurrentHealthText();
            UpdateLivesLeftText();
            _immortalityTimeCountdown = _immortalityTime;
            _blinkIntervalCountdown = _blinkInterval;
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
                _immortalityTimeCountdown -= Time.deltaTime;
                if (_immortalityTimeCountdown <= 0f)
                {
                    _immortalityTimeCountdown = _immortalityTime;
                    _isImmortal = false;
                    gameObject.GetComponent<SpriteRenderer>().color = visible;
                }
            }
        }

        protected override void Die()
        {
            if (Health.IsDead)
            {
                PlayerDeath();
            }
        }
    }
}
