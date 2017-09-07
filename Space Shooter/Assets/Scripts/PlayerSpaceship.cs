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

        // Use this for initialization
        void Start()
        {
            //rotationZ = transform.rotation.z;
        }

        // Update is called once per frame
        override protected void Update()
        {
            try
            {
                Move();
            } catch(System.NotImplementedException exception) {
                Debug.Log(exception.Message);
            } catch(System.Exception exception)
            {
                Debug.LogException(exception);
            }
        }

        protected override void Move()
        {
            Vector3 inputVector = new Vector3(Input.GetAxis(HorizontalAxisName), Input.GetAxis(VerticalAxisName));

            transform.Translate(-inputVector * Speed * Time.deltaTime);
        }
    }
}
