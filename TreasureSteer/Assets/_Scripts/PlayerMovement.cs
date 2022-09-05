using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float input, rotationInput;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float _velocity, _accelerationRate, _decelerationRate, _torque, _friction, _rotationSpeed;
    [SerializeField] private float _rotationAngle, _acceleration, _accBost = 1f;

    void Update()
    {
        rotationInput = Input.GetAxisRaw("Horizontal");
        input = Input.GetAxisRaw("Vertical");
        GetAcceleration();
        rb2D.velocity = transform.up * _acceleration * _velocity * _accBost;

        GetRotationAngle();
        transform.Rotate(new Vector3(0, 0, -(_rotationAngle)) * Time.deltaTime * _rotationSpeed, Space.World);

        Propulsion();
    }


    private void GetAcceleration()
    {
        switch (input)
        {
            case 1:
                _acceleration += _accelerationRate;
                if (_acceleration > 1f) _acceleration = 1f;
                break;
            case 0:
                if(_acceleration > 0)
                {
                    _acceleration -= _decelerationRate;
                    if (_acceleration < 0.001f) _acceleration = 0;
                }
                if(_acceleration < 0)
                {
                    _acceleration += _decelerationRate;
                    if(_acceleration > -0.001f) _acceleration = 0;
                }
                break;
            case -1:
                _acceleration -= _accelerationRate;
                if (_acceleration < -1f) _acceleration = -1f;
                break;
        }
    }

    private void GetRotationAngle()
    {
        switch (rotationInput)
        {
            case 1:
                if (_rotationAngle < 1)
                {
                    _rotationAngle += _torque;
                    if (_rotationAngle > 0.99f) _rotationAngle = 1f;
                }
                break;
            case 0:
                if (_rotationAngle > 0)
                {
                    _rotationAngle -= _friction;
                    if (_rotationAngle < 0.001f) _rotationAngle = 0f;
                }
                if (_rotationAngle < 0)
                {
                    _rotationAngle += _friction;
                    if (_rotationAngle > -0.001f) _rotationAngle = 0f;
                }
                break;
            case -1:
                if (_rotationAngle > -1)
                {
                    _rotationAngle -= _torque;
                    if (_rotationAngle < -0.99f) _rotationAngle = -1f;
                }
                break;
        }

    }

    public void TeleportPlayerY()
    {
        transform.position *= new Vector2(1, -1);
    }

    public void TeleportPlayerX()
    {
        transform.position *= new Vector2(-1, 1); 
    }

    private void Propulsion()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            _accBost = 8f;
            Debug.Log("BOOST");
        } 

        _accBost = _accBost * 0.5f * Time.deltaTime;
        if (_accBost < 1.01f) _accBost = 1; 
    }
}
