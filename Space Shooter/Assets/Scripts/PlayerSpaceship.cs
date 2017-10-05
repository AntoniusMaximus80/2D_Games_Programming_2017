using UnityEngine;
using UnityEngine.UI;


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

        private void Start()
        {
            health = gameObject.GetComponent<Health>();
            UpdateCurrentHealthText();
        }

        private void UpdateCurrentHealthText()
        {
            _currentHealthText.text = "" + health.CurrentHealth.ToString();
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

            /*// Decrease health every 10 frames.
            _framesToOneDamage--;
            if (_framesToOneDamage == 0) {
                _framesToOneDamage = 10;
                health.DecreaseHealth(5);
            }*/

            // Update the UI text.
            UpdateCurrentHealthText();
        }
    }
}
