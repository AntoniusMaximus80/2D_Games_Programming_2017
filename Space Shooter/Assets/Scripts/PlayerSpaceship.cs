using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class PlayerSpaceship : SpaceshipBase
    {
        public const string HorizontalAxisName = "Horizontal";
        public const string VerticalAxisName = "Vertical";

        protected override void Move()
        {
            Vector3 inputVector = new Vector3(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));

            transform.Translate(-inputVector * Speed * Time.deltaTime);
        }

        protected override void Update()
        {
            base.Update();
            
            if (Input.GetButton("Fire1"))
            {
                Shoot();
            }
        }
    }
}
