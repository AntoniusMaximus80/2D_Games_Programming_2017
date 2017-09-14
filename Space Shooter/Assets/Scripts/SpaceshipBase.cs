using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpaceShooter
{
    public abstract class SpaceshipBase : MonoBehaviour
    {
        // SerializeField attribute forces Unity to serialize variable
        // in order to make it editable inside the editor.
        [SerializeField]
        private float _speed = 4f;

        private Weapon[] _weapons;

        public float Speed
        {
            get { return _speed; }
            protected set { _speed = value; }
        }

        public Weapon[] Weapons
        {
            get
            {
                return _weapons;
            }
        }

        protected abstract void Move();

        protected virtual void Update()
        {
            Move();
        }

        protected virtual void Awake()
        {
            _weapons = GetComponentsInChildren<Weapon>(includeInactive:true);
        }

        protected virtual void Shoot()
        {
            foreach(Weapon weapon in Weapons)
            {
                weapon.Shoot();
            }
        }
    }
}
