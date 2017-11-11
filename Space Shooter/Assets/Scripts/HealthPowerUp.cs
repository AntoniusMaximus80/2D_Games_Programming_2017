using UnityEngine;

namespace SpaceShooter
{
    public class HealthPowerUp : MonoBehaviour, IHealProvider
    {
        // Adjustable amount of time how long the power up will exist.
        [SerializeField]
        private float _powerUpLifetime;

        // Adjustable amount how much the health power up will heal.
        [SerializeField]
        private int healAmount;

        // Update is called once per frame
        protected void Update()
        {
            _powerUpLifetime -= Time.deltaTime;
            // When the power up lifetime runs out, destroy the GameObject.
            if (_powerUpLifetime <= 0f)
            {
                Destroy(gameObject);
            }
            // Slowly translate the power up down.
            transform.Translate(Vector2.down * Time.deltaTime * 2f);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IHealReceiver healReceiver = collision.GetComponent<IHealReceiver>();
            if (healReceiver != null)
            {
                if (collision.GetComponent<PlayerSpaceship>() != null)
                {
                    healReceiver.ReceiveHeal(HealTarget());
                    Destroy(gameObject);
                }
            } else
            {
                // Power ups can only collide with the player or the destroyers. If it collides with a non-PlayerSpaceShip, destroy it.
                Destroy(gameObject);
            }
        }

        public int HealTarget()
        {
            return healAmount;
        }
    }
}
