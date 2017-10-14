using UnityEngine;

namespace SpaceShooter
{
    public class Projectile : MonoBehaviour, IDamageProvider
    {
        [SerializeField]
        private int _damage;

        [SerializeField]
        private float _speed;

        private Rigidbody2D _ridigbody2D;
        private Vector2 _direction;
        private bool _isLaunched = false;
        private Weapon _weapon;
        private AudioSource _audio;

        protected virtual void Awake()
        {
            _ridigbody2D = GetComponent<Rigidbody2D>();
            _audio = GetComponent<AudioSource>();

            if (_ridigbody2D == null)
            {
                Debug.LogError("No Rigidbody2D component was found from the GameObject.");
            }
        }

        protected void FixedUpdate()
        {
            if (!_isLaunched)
            {
                return;
            }

            Vector2 velocity = _direction * _speed;
            Vector2 currentPosition = new Vector2(transform.position.x,
                transform.position.y);
            Vector2 newPosition = currentPosition + velocity * Time.fixedDeltaTime;
            _ridigbody2D.MovePosition(newPosition);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            IDamageReceiver damageReceiver = other.GetComponent<IDamageReceiver>();
            if (damageReceiver != null)
            {
                //Debug.Log("Hit a damage receiver.");
                // Check if the other GameObject is a PlayerSpaceship.
                if (other.gameObject.GetComponent<PlayerSpaceship>() != null)
                {
                    // If it is, check if it is immortal.
                    if (!other.gameObject.GetComponent<PlayerSpaceship>()._isImmortal)
                    {
                        damageReceiver.TakeDamage(GetDamage());
                    }

                } else
                {
                    damageReceiver.TakeDamage(GetDamage());
                }
            }
            if (!_weapon.DisposeProjectile(this))
            {
                Debug.LogError("Couldn't dispose projectile.");
                Destroy(gameObject);
            }
        }

        public void Launch(Weapon weapon, Vector2 direction)
        {
            _weapon = weapon;
            _direction = direction;
            _isLaunched = true;
            _audio.PlayOneShot(_audio.clip, 1f);
        }

        public int GetDamage()
        {
            return _damage;
        }
    }
}
