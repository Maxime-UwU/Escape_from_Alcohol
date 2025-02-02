using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    [SerializeField]
    private CharacterMovement m_Movement;

    [SerializeField]
    private ShootBeer m_Beer;

    // Update is called once per frame
    void Update()
    {
        float dirX = 0;
        float dirY = 0;
        float rotation = 0;

        if (Keyboard.current.aKey.isPressed)
        {
            dirX = -1;
            rotation = 90;
        }
        else if (Keyboard.current.dKey.isPressed)
        {
            dirX = 1;
            rotation = 270;
        }
        else if(Keyboard.current.wKey.isPressed)
        {
            dirY = 1;
            rotation = 0;
        }
        else if (Keyboard.current.sKey.isPressed)
        {
            dirY = -1;
            rotation = 180;
        }
        else if (Keyboard.current.eKey.wasPressedThisFrame)
        {
            m_Beer.Shoot();
        }

        m_Movement.MoveX(dirX, rotation);
        m_Movement.MoveY(dirY, rotation);


    }
}
