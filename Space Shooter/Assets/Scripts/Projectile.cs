using System.Collections;
using System.Collections.Generic;
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

        protected virtual void Awake()
        {
            _ridigbody2D = GetComponent<Rigidbody2D>();

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
                Debug.Log("Hit a damage receiver.");
                damageReceiver.TakeDamage(GetDamage());
                Destroy(gameObject);
            }
        }

        public void Launch(Vector2 direction)
        {
            _direction = direction;
            _isLaunched = true;
        }

        public int GetDamage()
        {
            return _damage;
        }
    }
}
