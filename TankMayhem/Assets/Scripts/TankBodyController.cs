using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankBodyController : MonoBehaviour
{

    public float speed;
    public Joystick tankJoyStick;
    public Rigidbody rb;

    private void Start()
    {
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        move();
    }

    void move()
    {
        Vector3 direction = Vector3.forward * tankJoyStick.Vertical + Vector3.right * tankJoyStick.Horizontal;
        Vector3 movement = direction * speed * Time.deltaTime;
        if (movement.x != 0 || movement.y != 0 || movement.z != 0) {
            rb.MovePosition(rb.position + movement);
            rb.rotation = Quaternion.LookRotation(direction);
        }
    }
}
