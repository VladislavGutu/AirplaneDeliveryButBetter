using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirplaneRotate : MonoBehaviour
{
    public float rotationSpeed = 5f;
    
    void Update()
    {
        if (GameController.IsState(GameState.InMainMenu))
        {
#if UNITY_EDITOR
            // Get input from the horizontal axis (e.g., arrow keys or joystick)
            float horizontalInput = Input.GetAxis("Horizontal");
#endif

#if UNITY_SWITCH
            float horizontalInput = Gamepad.current?.leftStick.x.ReadValue() ?? 0f;
#endif
            // Rotate the object based on the input
            if (horizontalInput > 0.1f || horizontalInput < -0.1f)
                Rotate(horizontalInput);
        }
        else
        {
            this.enabled = false;
            transform.rotation = new Quaternion(0,0,0,0);
        }

        void Rotate(float input)
        {
            // Calculate the rotation amount based on input and speed
            float rotationAmount = input * rotationSpeed * Time.deltaTime;

            // Apply the rotation to the object around the Y-axis
            transform.Rotate(Vector3.up, rotationAmount);
        }
    }
}
