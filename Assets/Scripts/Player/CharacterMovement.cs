using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Rigidbody2D _rigidBody;

    [SerializeField, Range(0f, 1f)]
    private float m_Deceleration;

    [SerializeField]
    private float m_MoveSpeed;

    public GameObject ShootingPoint;
    public GameObject Player;

    private float _dirX = 0;
    private float _dirY = 0;
    private float _rotation = 0;
    private bool m_FacingRight;

    [SerializeField]
    private LayerMask m_GroundMask;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
        ShootingPoint = GameObject.Find("ShootingPoint");
        Player = GameObject.Find("Character");
    }

    public void MoveX(float dirX, float rotation)
    {
        _dirX = dirX;
        _rotation = rotation;
    }

    public void MoveY(float dirY, float rotation)
    {
        _dirY = dirY;
        _rotation = rotation;
    }

    private void FixedUpdate()
    {
        if (Mathf.Abs(_dirX) > 0.01f || Mathf.Abs(_dirY) > 0.01f)
        {
            _rigidBody.velocity = new Vector2(_dirX * m_MoveSpeed, _dirY * m_MoveSpeed);
            //ShootingPoint.transform.position = new Vector2( Player.transform.position.x + _dirX, Player.transform.position.y + _dirY);
            //this.transform.Rotate = new Vector2(180, 0);
            this.transform.rotation = Quaternion.Euler(0, 0, _rotation);

        }
        else
        {
            _rigidBody.velocity = new Vector2(_rigidBody.velocity.x * m_Deceleration, _rigidBody.velocity.y * m_Deceleration);
        }
    }

    private void Update()
    {

    }
}
