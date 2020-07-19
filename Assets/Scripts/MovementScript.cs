﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class MovementScript : MonoBehaviour
    {
        public float walkMovementSpeed;
        public float xMin, xMax, zMin, zMax;

        private float movementSpeed;
        private bool facingRight;
        private Rigidbody rigidbody;
        
        private void Update()
        {
            movementSpeed = walkMovementSpeed;
        }

        private void FixedUpdate()
        {
            // Movement
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

            rigidbody.velocity = movement * movementSpeed;
            rigidbody.position = new Vector3(
                Mathf.Clamp(rigidbody.position.x, xMin, xMax),
                transform.position.y,
                Mathf.Clamp(rigidbody.position.z, zMin, zMax)
            );

            if (moveHorizontal > 0 && !facingRight)
                Flip();
            else if (moveHorizontal < 0 && facingRight)
                Flip();
        }

        private void Flip()
        {
            facingRight = !facingRight;
            Vector3 thisScale = transform.localScale;
            thisScale.x *= -1;
            transform.localScale = thisScale;
        }
    }
}