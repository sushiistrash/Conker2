using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerClimbing : MonoBehaviour
{
    [SerializeField] private float _climpSpeed = 8f;
    private bool _isClimbing;
    [HideInInspector] public bool IsOnLadder;

    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    private float _verticalInput;

    private void FixedUpdate()
    {
        Climb();
    }

    private void Update()
    {
        _verticalInput = Input.GetAxisRaw("Vertical");
        if (_verticalInput != 0f)
        {
            _isClimbing = true;
        }
        else
        {
            _isClimbing = false;
        }

        if (!_isClimbing && IsOnLadder)
        {
            _animator.speed = 0f;
        }
        else
        {
            _animator.speed = 1f;
        }

        _animator.SetBool("isOnLadder", IsOnLadder);
        _animator.SetBool("isClimbing", _isClimbing);
    }

    public void Climb()
    {
        if (IsOnLadder)
        {
            _rigidbody.gravityScale = 0f;
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _verticalInput * _climpSpeed);
        }
        else
        {
            _rigidbody.gravityScale = 5f;
        }
    }
}
