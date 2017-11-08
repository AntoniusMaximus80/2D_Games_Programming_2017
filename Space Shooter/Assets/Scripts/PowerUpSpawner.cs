using UnityEngine;

namespace SpaceShooter
{
    public class PowerUpSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _healthPowerUpPrefab;

        [SerializeField]
        private GameObject _weaponPowerUpPrefab;

        public void SpawnPowerUp(EnemySpaceship enemySpaceship, string powerUpType)
        {
            if (powerUpType == "health")
            {
                Instantiate(_healthPowerUpPrefab, enemySpaceship.transform.position, new Quaternion());
            }
            if (powerUpType == "weapon")
            {
                Instantiate(_weaponPowerUpPrefab, enemySpaceship.transform.position, new Quaternion());
            }
        }
    }
}
