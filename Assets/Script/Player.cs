using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Player : Character
{
    [SerializeField] private FloatingJoystick joystick;
    private Vector3 moveVector;

    protected override void Update()
    {
        Move();
        //this.GetComponent<Rigidbody>().AddForce(Physics.gravity * 10, ForceMode.Acceleration);
        base.Update();
    }

    public void OnInit()
    {

    }

    public override void Move()
    {
        float checkZ = joystick.Vertical * moveSpeed;

        if (!CanMoveForward(checkZ))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            return;
        }

        moveVector = Vector3.zero;
        moveVector.x = joystick.Horizontal * rotateSpeed;
        moveVector.z = joystick.Vertical * moveSpeed;


        if (joystick.Horizontal != 0 || joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);

            // Allow forward movement
            GetComponent<Rigidbody>().velocity = new Vector3(moveVector.x, GetComponent<Rigidbody>().velocity.y, moveVector.z);
        }
        else if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            //anim idle
        }
        //}

    }
   

}
