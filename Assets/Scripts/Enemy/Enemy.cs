using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _leftPoint, _rightPoint;

    [SerializeField, Range(3f, 5f)] private float _minMoveTime;
    [SerializeField, Range(5f, 7f)] private float _maxMoveTime;
    [SerializeField, Range(.5f, 2f)] private float _minWaitTime;
    [SerializeField, Range(1.5f, 3f)] private float _maxWaitTime;
    private float _moveCount, _waitCount;

    private DirectionState _directionState = DirectionState.Right;

    private void Awake()
    {
        _directionState = DirectionState.Right;
        _leftPoint.parent = null;
        _rightPoint.parent = null;

        _moveCount = Random.Range(_minMoveTime, _maxMoveTime);
        _waitCount = Random.Range(_minWaitTime, _maxWaitTime);
    }

    private void Update()
    {
        if (_moveCount > 0)
        {
            Move();
            _animator.SetBool("isMoving", true);
        }
        else if (_waitCount > 0)
        {
            Wait();
            _animator.SetBool("isMoving", false);
        }
        FlipEnemy();
    }

    private void Move()
    {
        _moveCount -= Time.deltaTime;

        MoveRight();
        MoveLeft();

        if (_moveCount <= 0)
        {
            _waitCount = Random.Range(_minWaitTime, _maxWaitTime);
            Wait();
        }
    }

    private void Wait()
    {
        _waitCount -= Time.deltaTime;
        _rigidbody.velocity = new Vector2(0f, _rigidbody.velocity.y);

        if (_waitCount <= 0)
        {
            _moveCount = Random.Range(_minMoveTime, _maxMoveTime);
            Move();
        }
    }

    private void MoveRight()
    {
        if (_directionState == DirectionState.Right)
        {
            _rigidbody.velocity = new Vector2(_moveSpeed, _rigidbody.velocity.y);

            if (transform.position.x > _rightPoint.position.x)
            {
                _directionState = DirectionState.Left;
                MoveLeft();
            }
        }
    }

    private void MoveLeft()
    {
        if (_directionState == DirectionState.Left)
        {
            _rigidbody.velocity = new Vector2(-_moveSpeed, _rigidbody.velocity.y);

            if (transform.position.x < _leftPoint.position.x)
            {
                _directionState = DirectionState.Right;
                MoveRight();
            }
        }
    }

    private void FlipEnemy()
    {
        if (_rigidbody.velocity.x > 0)
        {
            _spriteRenderer.flipX = true;
        }
        else if (_rigidbody.velocity.x < 0)
        {
            _spriteRenderer.flipX = false;
        }
    }

    private enum DirectionState
    {
        Right,
        Left,
    }
}
