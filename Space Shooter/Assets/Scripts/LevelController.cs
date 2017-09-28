using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelController : MonoBehaviour
    {
        public static LevelController Current
        {
            get; private set;
        }

        [SerializeField]
        private Spawner _enemySpawner;

        [SerializeField]
        private GameObject[] _enemyMovementTargets;

        [SerializeField]
        private float _spawnInterval = 1f;

        [SerializeField, Tooltip("The time before the first spawn.")]
        private float _waitToSpawn;

        [SerializeField]
        private int _maximumEnemyUnitsToSpawn;

        private int _enemyCount;

        [SerializeField]
        private GameObjectPool _playerProjectilePool;

        [SerializeField]
        private GameObjectPool _enemyProjectilePool;

        protected void Awake()
        {
            if (_enemySpawner == null)
            {
                Debug.Log("No reference to enemy spawner.");
                //_enemySpawner = GameObject.FindObjectOfType<Spawner>();

                _enemySpawner = GetComponentInChildren<Spawner>();

                //_enemySpawner = transform.Find("EnemySpawner").gameObject.GetComponent<Spawner>();

                /*Transform childTransform = transform.Find("EnemySpawner");
                if (childTransform != null)
                {
                    _enemySpawner = childTransform.gameObject.GetComponent<Spawner>();
                }*/
            }

            if (Current == null)
            {
                Current = this;
            }
            else
            {
                Debug.LogError("Multiple LevelControllers detected in the scene!");
            }
        }

        private void Start()
        {
            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(_waitToSpawn);
            while (_enemyCount <= _maximumEnemyUnitsToSpawn)
            {
                EnemySpaceship enemy = SpawnEnemyUnit();
                if (enemy != null)
                {
                    _enemyCount++;
                }
                else
                {
                    Debug.LogError("Could not spawn an enemy.");
                    yield break;
                }
                yield return new WaitForSeconds(_spawnInterval);
            }
        }

        private EnemySpaceship SpawnEnemyUnit()
        {
            GameObject spawnedEnemyObject = _enemySpawner.Spawn();
            EnemySpaceship enemyShip = spawnedEnemyObject.GetComponent<EnemySpaceship>();

            if (enemyShip != null)
            {
                enemyShip.SetMovementTargets(_enemyMovementTargets);
            }
            return enemyShip;
        }

        public Projectile GetProjectile(SpaceshipBase.Type type)
        {
            GameObject result = null;

            if (type == SpaceshipBase.Type.Player)
            {
                result = _playerProjectilePool.GetPooledObject();
            }
            else
            {
                result = _enemyProjectilePool.GetPooledObject();
            }

            if (result != null)
            {
                return result.GetComponent<Projectile>();
            } else
            {
                return null;
            }
        }

        public bool ReturnProjectile(SpaceshipBase.Type type, Projectile projectile)
        {
            if (type == SpaceshipBase.Type.Player)
            {
                return _playerProjectilePool.ReturnObject(projectile.gameObject);
            }
            else
            {
                return _enemyProjectilePool.ReturnObject(projectile.gameObject);
            }
        }
    }

}