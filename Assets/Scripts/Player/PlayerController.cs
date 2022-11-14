using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float _speed = 4f;
    private Rigidbody2D _rigidbody;
    private Vector3 _playerMovement;
    private Animator _animator;

    private bool _disableMoviment = false;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (!_disableMoviment)
        {
            _playerMovement = Vector3.zero;
            _playerMovement.x = Input.GetAxisRaw("Horizontal");
            _playerMovement.y = Input.GetAxisRaw("Vertical");
            _playerMovement = Vector3.ClampMagnitude(_playerMovement, 1f);

            UpdateAnimationAndMove();
        }
    }

    private void UpdateAnimationAndMove()
    {
        if (_playerMovement != Vector3.zero)
        {
            MoveCharacter();
            _animator.SetFloat("MoveX", _playerMovement.x);
            _animator.SetFloat("MoveY", _playerMovement.y);
            _animator.SetBool("Walking", true);
        }
        else
        {
            _animator.SetBool("Walking", false);
        }
    }

    private void MoveCharacter()
    {
        _rigidbody.MovePosition(transform.position + _playerMovement * _speed * Time.deltaTime);
    }

    public void SetDisableMoviment(bool setting)
    {
        _disableMoviment = setting;
    }
}
