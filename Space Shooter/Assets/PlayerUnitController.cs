using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace SpaceShooter
{
    public class PlayerUnitController : MonoBehaviour
    {

        //float rotationZ;

        float speed = 4f;

        // Use this for initialization
        void Start()
        {
            //rotationZ = transform.rotation.z;
        }

        // Update is called once per frame
        void Update()
        {
            Vector3 movementVector = GetMovementVector();
            transform.Translate(-movementVector * speed * Time.deltaTime);
            /*if (Input.GetKey(KeyCode.A))
            {
                rotationZ += 5f;
            }

            if (Input.GetKey(KeyCode.D))
            {
                rotationZ += -5f;
            }

            transform.rotation = Quaternion.Euler(0, 0, rotationZ);*/
        }

        private Vector3 GetMovementVector()
        {
            Vector3 movementVector = Vector3.zero;

            if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                movementVector += Vector3.left;
            }

            if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                movementVector += Vector3.right;
            }

            if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
            {
                movementVector += Vector3.up;
            }

            if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
            {
                movementVector += Vector3.down;
            }

            return movementVector;
        }
    }
}
