using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private PauseMenu _pauseMenu;

    [SerializeField] private float _jumpForce;
    private bool _isGrounded = false;
    [SerializeField] private Transform _groundCheckTransform;
    [SerializeField] private float _groundCheckRadius;
    [SerializeField] private LayerMask _collisionMask;
    [SerializeField] private int _maxJumps = 2;
    private int _jumpsLeft;

    [SerializeField] private float _knockBackLength, _knockBackForce;
    private float _knockBackCounter;

    [SerializeField] private float _bounceForce;

    private DirectionState _direction = DirectionState.Right;

    [HideInInspector] public bool StopInput;

    private void Awake()
    {
        _jumpsLeft = _maxJumps;
        _direction = DirectionState.Right;
    }

    private void Update()
    {
        if (_pauseMenu.IsPaused) return;
        if (StopInput)
        {
            _animator.SetBool("isFalling", false);
            _animator.SetBool("isGrounded", true);
            _animator.Play("PlayerRun");
            return;
        }
        if (_knockBackCounter <= 0)
        {
            if (Input.GetKey(KeyCode.C))
            {
                Crouch();
            }
            else
            {
                Move();
            }

            _isGrounded = Physics2D.OverlapCircle(_groundCheckTransform.position, _groundCheckRadius, _collisionMask);

            if (_isGrounded && _rigidbody.velocity.y <= 0.0001)
            {
                _jumpsLeft = _maxJumps;
            }

            DoubleJumpLogic();

            FlipPlayer();
        }
        else
        {
            _knockBackCounter -= Time.deltaTime;
            if (_direction == DirectionState.Right)
            {
                _rigidbody.velocity = new Vector2(-_knockBackForce, _rigidbody.velocity.y);
            }
            else if (_direction == DirectionState.Left)
            {
                _rigidbody.velocity = new Vector2(_knockBackForce, _rigidbody.velocity.y);
            }
        }

        UpdateStateAndAnimation();
    }

    private void Move()
    {
        _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed, _rigidbody.velocity.y);
    }

    private void Jump()
    {
        AudioSystem.Instance.Play("Player Jump");
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void Crouch()
    {
        _rigidbody.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * _moveSpeed * 0.5f, _rigidbody.velocity.y);
    }

    private void DoubleJumpLogic()
    {
        if (Input.GetButtonDown("Jump") && _jumpsLeft > 0)
        {
            Jump();
            _jumpsLeft--;
        }
    }

    private void UpdateStateAndAnimation()
    {
        _animator.SetBool("isGrounded", _isGrounded);
        _animator.SetFloat("moveSpeed", Mathf.Abs(_rigidbody.velocity.x));

        if (Input.GetKey(KeyCode.C))
        {
            _animator.SetBool("isCrouching", true);
        }
        else
        {
            _animator.SetBool("isCrouching", false);
        }

        if (_rigidbody.velocity.y >= 0)
        {
            _animator.SetBool("isFalling", false);
        }
        else
        {
            _animator.SetBool("isFalling", true);
        }
    }

    private void FlipPlayer()
    {
        if (_rigidbody.velocity.x > 0)
        {
            _spriteRenderer.flipX = false;
            _direction = DirectionState.Right;
        }
        else if (_rigidbody.velocity.x < 0)
        {
            _spriteRenderer.flipX = true;
            _direction = DirectionState.Left;
        }
    }

    public void KnockBack()
    {
        _knockBackCounter = _knockBackLength;
        _rigidbody.velocity = new Vector2(0f, _knockBackForce);

        _animator.SetTrigger("hurt");
        AudioSystem.Instance.Play("Player Hurt");
    }

    public void Bounce()
    {
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _bounceForce);
    }

    private enum DirectionState
    {
        Right,
        Left,
    }
}
