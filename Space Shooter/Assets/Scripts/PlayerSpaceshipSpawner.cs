using UnityEngine;

namespace SpaceShooter
{
    public class PlayerSpaceshipSpawner : MonoBehaviour
    {
        [SerializeField]
        private GameObject _playerSpaceshipPrefab;

        private void Start()
        {
            Instantiate(_playerSpaceshipPrefab, new Vector2(0f, -4f), new Quaternion(0f, 0f, 180f, 0f));
        }
    }
}