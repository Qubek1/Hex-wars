using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlayerInterface
{
    public class CameraController : MonoBehaviour
    {
        public Vector2 velocity;
        public float acceleration;
        public float maxSpeed;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            Vector2 direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            if (direction == Vector2.zero)
            {
                direction = velocity.normalized * acceleration * Time.deltaTime * 2f;
                if (velocity.magnitude < direction.magnitude)
                    velocity = Vector2.zero;
                else
                    velocity -= direction;
            }
            else
            {
                direction.Normalize();
                velocity += direction * acceleration * Time.deltaTime;
                if (velocity.magnitude > maxSpeed)
                    velocity = velocity.normalized * maxSpeed;
            }
            Vector3 NewPos = new Vector3(velocity.x * Time.deltaTime, 0, velocity.y * Time.deltaTime);
            transform.position += NewPos;
        }
    }
}