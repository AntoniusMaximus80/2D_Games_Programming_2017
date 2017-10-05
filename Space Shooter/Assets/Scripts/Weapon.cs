using UnityEngine;
using System.Collections;

namespace SpaceShooter
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField]
        private float _cooldownTime = 0.5f;

        [SerializeField]
        private Projectile _projectilePrefab;

        private float _timeSinceShot = 0f;
        private bool _isInCooldown = false;
        private SpaceshipBase _owner;

        public void Init(SpaceshipBase owner)
        {
            _owner = owner;
        }

        public bool Shoot()
        {
            if (_isInCooldown) {
                return false;
            }
            Projectile projectile = LevelController.Current.GetProjectile(_owner.UnitType);
            if (projectile != null)
            {
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.Launch(this, transform.up);
            }
            //Projectile projectile = Instantiate(_projectilePrefab, transform.position, transform.rotation);
            _isInCooldown = true;
            _timeSinceShot = 0f;
            return true;
        }

        public bool DisposeProjectile(Projectile projectile)
        {
            return LevelController.Current.ReturnProjectile(_owner.UnitType, projectile);
        }

        void Update()
        {
            if (_isInCooldown)
            {
                _timeSinceShot += Time.deltaTime;
                if (_timeSinceShot >= _cooldownTime)
                {
                    _isInCooldown = false;
                }
            }
        }
    }
}
