using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float dodgeSpeed;
    public float xLimitation;

    float xInput;

    // Update is called once per frame
    void Update()
    {
        xInput = Input.GetAxis("Horizontal");
        TouchInput();

        transform.Translate(xInput * dodgeSpeed * Time.deltaTime, 0, 0);
        // Translate moves the object.
        // Time.deltaTime makes framely independent, whether it will run on
        // fast or slow computer object will move with the same speed.

        float limitedX = Mathf.Clamp(transform.position.x, -xLimitation, xLimitation);
        transform.position = new Vector3(limitedX, transform.position.y, transform.position.z);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Obstacle")
        {
            GameManager.instance.Restart();
        }
    }

    // For touch input or pressing the mouse button input controlling
    void TouchInput()
    {
        if(Input.GetMouseButton(0))
        {
            Vector3 touchPosition = Input.mousePosition;

            float middle = Screen.width / 2;

            if (touchPosition.x < middle)
            {
                xInput = -1;
            }
            else
            {
                xInput = 1;
            }
        }
    }
}
