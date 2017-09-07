using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public class LevelController : MonoBehaviour
    {
        [SerializeField]
        private Spawner _enemySpawner;

        [SerializeField]
        private GameObject[] _enemyMovementTargets;

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

            SpawnEnemyUnit();
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
    }

}