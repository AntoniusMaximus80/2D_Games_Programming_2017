using UnityEngine;
using System.Collections.Generic;

namespace SpaceShooter
{
    public class GameObjectPool : MonoBehaviour
    {
        [SerializeField]
        private int _poolSize;
        [SerializeField]
        private GameObject _objectPrefab;
        [SerializeField]
        private bool _shouldGrow;

        private List<GameObject> _pool;

        protected void Awake()
        {
            _pool = new List<GameObject>(_poolSize);

            for (int i = 0; i < _poolSize; i++)
            {
                AddObjectToPool();
            }
        }

        private GameObject AddObjectToPool(bool isActive = false)
        {
            GameObject go = Instantiate(_objectPrefab);
            if (isActive)
            {
                Activate(go);
            }
            else
            {
                Deactivate(go);
            }

            _pool.Add(go);

            return go;
        }

        protected virtual void Deactivate(GameObject go)
        {
            go.SetActive(false);
        }

        protected virtual void Activate(GameObject go)
        {
            go.SetActive(true);
        }

        public GameObject GetPooledObject()
        {
            GameObject resultObject = null;
            for (int i = 0; i < _pool.Count; i++)
            {
                if (!_pool[i].activeSelf)
                {
                    resultObject = _pool[i];
                    break;
                }
            }

            if (resultObject == null && _shouldGrow)
            {
                resultObject = AddObjectToPool();
            }

            if (resultObject != null)
            {
                Activate(resultObject);
            }

            return resultObject;
        }

        public bool ReturnObject(GameObject go)
        {
            bool result = false;

            foreach (GameObject pooledObject in _pool)
            {
                Deactivate(go);
                result = true;
                break;
            }

            return result;
        }
    }
}