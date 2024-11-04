using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement m_Movement;

    // Update is called once per frame
    void Update()
    {
        float dirX = 0;
        float dirY = 0;

        if (Keyboard.current.aKey.isPressed)
        {
            dirX = -1;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            dirX = 1;
        }
        else if(Keyboard.current.wKey.isPressed)
        {
            dirY = 1;
        }
         else if (Keyboard.current.sKey.isPressed)
        {
            dirY = -1;
        }

        m_Movement.MoveX(dirX);
        m_Movement.MoveY(dirY);


    }
}
