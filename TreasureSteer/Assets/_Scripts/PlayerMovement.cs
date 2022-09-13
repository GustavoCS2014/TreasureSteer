using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float input, rotationInput, defaultVel, defaultRotation, defaultFriction, defaultDeceleration, defaultAccel;
    [SerializeField] private Rigidbody2D rb2D;
    [SerializeField] private float _velocity, _accelerationRate, _decelerationRate, _torque, _friction, _rotationSpeed;
    [SerializeField] private float _rotationAngle, _acceleration, _accBost = 1f;
    [SerializeField] private int _speedCounter = 0;

    private void Awake()
    {
        defaultVel = _velocity;
        defaultRotation = _rotationSpeed;
        defaultFriction = _friction;
        defaultDeceleration = _decelerationRate;
        defaultAccel = _accelerationRate;
    }

    void FixedUpdate()
    {
        rotationInput = Input.GetAxisRaw("Horizontal");
        input = Input.GetAxisRaw("Vertical");
        GetAcceleration();
        MovePlayer();

        GetRotationAngle();
        RotatePlayer();

    }

    public void RotatePlayer()
    {
        transform.Rotate(new Vector3(0, 0, -(_rotationAngle)) * Time.deltaTime * _rotationSpeed, Space.World);
    }

    public void MovePlayer()
    {
        rb2D.velocity = transform.up * _acceleration * _velocity * _accBost;
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

    public void IncreaseSpeed()
    {
        if(_speedCounter < 5)
        {
            _velocity *= 1.2f;
            _accelerationRate *= 1.2f;
            _decelerationRate *= 1.2f;
            _rotationSpeed *= 1.2f;
            _friction *= 1.2f;
            _speedCounter += 1;
        }
    }

    public void ResetSpeed()
    {
        _velocity = defaultVel;
        _rotationSpeed = defaultRotation;
        _decelerationRate = defaultDeceleration;
        _friction = defaultFriction;
        _accelerationRate = defaultAccel;
        _speedCounter = 0;
    }
}
