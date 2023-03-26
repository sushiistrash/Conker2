using UnityEngine;

public class BossTank : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform _bossTransform;
    [SerializeField] private Animator _animator;

    [Header("Movement")]
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Transform _leftPoint, _rightPoint;
    [SerializeField] private GameObject _mine;
    [SerializeField] private Transform _mineSpawnPoint;
    [SerializeField] private float _timeBetweenMines;
    [SerializeField] private float _mineSpeedUp = .2f;
    private float _mineCounter;

    private State _currentState = State.Shooting;
    private DirectionState _direction = DirectionState.Left;

    [Header("Shooting")]
    [SerializeField] private GameObject _bullet;
    [SerializeField] private Transform _firePoint;
    [SerializeField] private float _timeBetweenShots;
    [SerializeField] private float _shotSpeedUp = .2f;
    private float _shotCounter;
    
    [Header("Hurt")]
    [SerializeField] private float _hurtTime;
    private float _hurtCounter;
    [SerializeField] private GameObject _hitBox;


    [Header("Health")]
    [SerializeField] private int _maxHealth = 5;
    private int _currentHealth;
    [SerializeField] private GameObject _explosion, _winObject;
    private bool _isDefeated = false;

    private void Awake()
    {
        _currentHealth = _maxHealth;
        _currentState = State.Shooting;
        _direction = DirectionState.Left;
    }

    private void Update()
    {
        switch (_currentState)
        {
            case State.Shooting:
                _shotCounter -= Time.deltaTime;

                if (_shotCounter <= 0)
                {
                    _shotCounter = _timeBetweenShots;

                    GameObject newBullet = Instantiate(_bullet, _firePoint.position, _firePoint.rotation);

                    newBullet.transform.localScale = _bossTransform.localScale;
                }

                break;
            case State.Hurt:
                if (_hurtCounter > 0)
                {
                    _hurtCounter -= Time.deltaTime;
                }
                else
                {
                    _currentState = State.Moving;

                    _mineCounter = .4f;
                    
                    if (_isDefeated)
                    {
                        _currentState = State.Defeated;
                        _bossTransform.gameObject.SetActive(false);
                        Instantiate(_explosion, _bossTransform.position, Quaternion.identity);

                        AudioSystem.Instance.Play("Main Level");
                        _winObject.SetActive(true);
                    }
                }
                break;
            case State.Moving:
                if (_direction == DirectionState.Right)
                {
                    _bossTransform.position = Vector3.MoveTowards(_bossTransform.position, _rightPoint.position, _moveSpeed * Time.deltaTime);
                    if (_bossTransform.position.x >= _rightPoint.position.x)
                    {
                        _bossTransform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
                        _direction = DirectionState.Left;
                        EndMovement();
                    }
                }
                else
                {
                    _bossTransform.position = Vector3.MoveTowards(_bossTransform.position, _leftPoint.position, _moveSpeed * Time.deltaTime);
                    if (_bossTransform.position.x <= _leftPoint.position.x)
                    {
                        _bossTransform.localScale = new Vector3(-1.5f, 1.5f, 1.5f);
                        _direction = DirectionState.Right;
                        EndMovement();
                    }
                }

                _mineCounter -= Time.deltaTime;

                if (_mineCounter <= 0)
                {
                    _mineCounter = _timeBetweenMines;

                    GameObject newMine = Instantiate(_mine, _mineSpawnPoint.position, _mineSpawnPoint.rotation);
                    newMine.transform.parent = transform;
                }

                break;
        }

#if UNITY_EDITOR
        if (Input.GetKeyDown(KeyCode.H))
        {
            TakeHit();
        }
#endif
    }

    public void TakeHit()
    {
        _currentState = State.Hurt;
        _hurtCounter = _hurtTime;

        _animator.SetTrigger("hit");
        AudioSystem.Instance.Play("Boss Hit");

        BossTankMine[] mines = GetComponentsInChildren<BossTankMine>();
        if (mines.Length > 0)
        {
            foreach (BossTankMine mine in mines)
            {
                mine.Explode();
            }
        }

        _currentHealth--;

        if (_currentHealth <= 0)
        {
            _isDefeated = true;
        }
        else
        {
            _timeBetweenShots -= _shotSpeedUp;
            _timeBetweenMines -= _mineSpeedUp;
        }
    }

    private void EndMovement()
    {
        _currentState = State.Shooting;
        _shotCounter = 0;
        _animator.SetTrigger("stopMoving");
        _hitBox.SetActive(true);
    }

    private enum State
    {
        Shooting,
        Hurt,
        Moving,
        Defeated,
    }

    private enum DirectionState
    {
        Left,
        Right,
    }
}
