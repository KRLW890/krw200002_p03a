using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BashManager))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float _movementSpeed = 5f;
    [SerializeField] float _jumpHeight = 5f;
    [SerializeField] float _doubleJumpHeight = 1f;

    BashManager _bm;
    Rigidbody2D _rb;
    bool _isGrounded = false;
    bool _hasDoubleJump = true;

    // Start is called before the first frame update
    void Start()
    {
        _bm = GetComponent<BashManager>();
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // if bash isn't currently active, accept keyboard inputs and move normally
        if (!_bm.isBashActive())
        {
            _rb.velocity = new Vector2(_movementSpeed * Input.GetAxis("Horizontal") * _bm.getTimeFactor(), _rb.velocity.y * _bm.getTimeFactor());
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                jump();
            }
        }
        else
        {
            _rb.velocity = new Vector2(_rb.velocity.x * _bm.getTimeFactor(), _rb.velocity.y * _bm.getTimeFactor());
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            _bm.startBash(gameObject);
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _bm.releaseBash();
        }
    }

    void refreshMovement()
    {
        _hasDoubleJump = true;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _isGrounded = true;
            refreshMovement();
        }
    }

    bool jump()
    {
        if (_bm.isBashActive())
            return false;
        else if (_isGrounded)
        {
            _isGrounded = false;
            _rb.AddForce(new Vector2(0, _jumpHeight * _bm.getTimeFactor()));
            //_rb.velocity = new Vector2(_rb.velocity.x, _jumpHeight * _bm.getTimeFactor());
            return true;
        }
        else if (_hasDoubleJump)
        {
            _hasDoubleJump = false;
            _rb.AddForce(new Vector2(0, _doubleJumpHeight * _bm.getTimeFactor()));
            return true;
        }
        else
            return false;
    }
}
