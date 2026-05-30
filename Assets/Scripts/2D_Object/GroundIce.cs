using System;
using UnityEngine;

public class GroundIce : MonoBehaviour
{
    [Header("미끄러움 설정")]
    [SerializeField] private float _slipDirection = 1.0f;
    [SerializeField] private float _slipAcceleration = 15.0f;
    [SerializeField] private float _maxSlipSpeed = 8.0f;

    private float _currentSlipSpeed = 0f;
    private PlayerMove_2D _player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _player = collision.gameObject.GetComponent<PlayerMove_2D>();
        }
    }

    private void Update()
    {
        if (_player !=null)
        {
            _currentSlipSpeed += _slipDirection * _slipAcceleration * Time.deltaTime;

            if(_slipDirection > 0)
            {
                if (_currentSlipSpeed < 0f) _currentSlipSpeed = 0f;
                if (_currentSlipSpeed > _maxSlipSpeed) _currentSlipSpeed = _maxSlipSpeed;
            }
            else
            {
                if (_currentSlipSpeed > 0f) _currentSlipSpeed = 0f;
                if (_currentSlipSpeed < -_maxSlipSpeed) _currentSlipSpeed = -_maxSlipSpeed;
            }

            _player.SetExternalVelocityX(_currentSlipSpeed);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (_player != null)
            {
                _player = null;
            }
            _currentSlipSpeed = 0f;
        }
    }
}
