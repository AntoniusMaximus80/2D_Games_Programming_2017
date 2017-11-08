using UnityEngine;

namespace SpaceShooter
{
    public class WeaponPowerUp : PowerUpBase
    {
        [SerializeField]
        private float powerUpLifetime;

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
            if (collision.GetComponent<PlayerSpaceship>() != null)
            {
                collision.GetComponent<PlayerSpaceship>().ActivateWeaponPowerUp();
                Destroy(gameObject);
            } else
            {
                // Power ups can only collide with the player or the destroyers. If it collides with a non-PlayerSpaceShip, destroy it.
                Destroy(gameObject);
            }
        }
    }
}
