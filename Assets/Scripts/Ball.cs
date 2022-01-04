using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    float _speed = 20f;
    Rigidbody _rigidbody;
    Vector3 _velocity;
    public Vector3 rotator;
    Renderer _renderer;
    bool hasStarted = false;
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
        transform.Rotate(rotator * (transform.position.x + transform.position.y) * 0.1f);
    }

    private void Update()
    {
        transform.Rotate(rotator * Time.deltaTime);
        if (hasStarted == false)
        {
            LockBall();
            Launch();
        }
    }

    private void LockBall()
    {
        _rigidbody.MovePosition(new Vector3(Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, 0, 50)).x, -15, 0));
    }

    void Launch()
    {
        if (Input.anyKeyDown)
        {
            hasStarted = true;
            _rigidbody.velocity = Vector3.up * _speed;
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = _rigidbody.velocity.normalized * _speed;
        _velocity = _rigidbody.velocity;

        if (!_renderer.isVisible)
        {
            GameManager.Instance.Balls--;
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        _rigidbody.velocity = Vector3.Reflect(_velocity, collision.contacts[0].normal);
    }
}
