using System;
using UnityEngine;

namespace SpaceShooter
{
    public class EnemySpaceship : SpaceshipBase
    {
        public override Type UnitType
        {
            get
            {
                return Type.Enemy;
            }
        }

        [SerializeField]
        private float _reachDistance = 0.5f;

        [SerializeField]
        private int _powerUpLikelihood;

        private PowerUpSpawner _powerUpSpawner;

        private GameObject[] _movementTargets;
        private int _currentMovementTargetIndex = 0;

        public Transform CurrentMovementTarget
        {
            get
            {
                return _movementTargets[_currentMovementTargetIndex].transform;
            }
        }

        public void SetMovementTargets(GameObject[] movementTargets)
        {
            _movementTargets = movementTargets;
            _currentMovementTargetIndex = 0;
        }

        protected override void Move()
        {
            if (_movementTargets == null || _movementTargets.Length == 0)
            {
                return;
            }

            UpdateMovementTarget();

            Vector3 direction = (CurrentMovementTarget.position - transform.position).normalized;

            transform.Translate(direction * Speed * Time.deltaTime);
        }

        private void UpdateMovementTarget()
        {
            // Have we reached the movement target?
            if (Vector3.Distance(transform.position,
                CurrentMovementTarget.position) < _reachDistance)
            {
                if (_currentMovementTargetIndex != _movementTargets.Length - 1) {
                    _currentMovementTargetIndex++;
                } else
                {
                    _currentMovementTargetIndex = 0;
                }
            }
        }

        private void Start()
        {
            _powerUpSpawner = FindObjectOfType<PowerUpSpawner>();
        }

        protected override void Update()
        {
            base.Update();
            Shoot();
        }

        protected override void Die()
        {
            if (Health.IsDead)
            {
                int shouldISpawnPowerUp = UnityEngine.Random.Range(1, 100);
                if (shouldISpawnPowerUp <= _powerUpLikelihood) {
                    int whichPowerUpShouldISpawn = UnityEngine.Random.Range(0, 2);
                    if (whichPowerUpShouldISpawn == 0) {
                        _powerUpSpawner.SpawnPowerUp(this, "health");
                    } else
                    {
                        _powerUpSpawner.SpawnPowerUp(this, "weapon");
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
