using UnityEngine;

namespace SpaceShooter
{
    // The class must have this component.
    [RequireComponent(typeof(Health))]
    public abstract class SpaceshipBase : MonoBehaviour, IDamageReceiver
    {
        public enum Type
        {
            Player,
            Enemy
        }

        public abstract Type UnitType { get; }

        // SerializeField attribute forces Unity to serialize variable
        // in order to make it editable inside the editor.
        [SerializeField]
        private float _speed = 4f;

        private Weapon[] _weapons;

        // An autoproperty. Backing fields are generated automatically by the compiler.
        public IHealth Health { get; protected set; }

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
            Health = GetComponent<IHealth>();
            foreach (Weapon weapon in _weapons)
            {
                weapon.Init(this);
            }
        }

        protected virtual void Shoot()
        {
            foreach(Weapon weapon in Weapons)
            {
                if (weapon.isActiveAndEnabled) {
                    weapon.Shoot();
                }
            }
        }

        protected abstract void Die();


        public void TakeDamage(int amount)
        {
            //Debug.Log("TakeDamage:" + amount);
            Health.DecreaseHealth(amount);
            //Debug.Log(name + " " + Health.CurrentHealth.ToString());
            Die();
        }

        protected Projectile GetPooledProjectile()
        {
            return LevelController.Current.GetProjectile(UnitType);
        }

        protected bool ReturnPooledProjectile(Projectile projectile)
        {
            return LevelController.Current.ReturnProjectile(UnitType, projectile);
        }
    }
}