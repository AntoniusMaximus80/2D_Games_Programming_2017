using UnityEngine;

namespace SpaceShooter
{
    public class HealthPowerUp : PowerUpBase, IHealProvider
    {
        [SerializeField]
        private float powerUpLifetime;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            powerUpLifetime -= Time.deltaTime;
            if (powerUpLifetime <= 0f)
            {
                Destroy(gameObject);
            }
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
            return 20;
        }
    }
}
