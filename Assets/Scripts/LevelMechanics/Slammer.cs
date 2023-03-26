using UnityEngine;

public class Slammer : MonoBehaviour
{
    [SerializeField] private Transform _slammer, _slammerTarget, _player;
    private Vector3 _startPoint;

    [SerializeField] private float _slamSpeed, _waitAfterSlam, _resetSpeed;
    private float _waitCounter;
    private bool _isSlamming, _isResetting;

    void Start()
    {
        _startPoint = _slammer.position;
        _slammerTarget.parent = null;
    }

    void Update()
    {
        if (!_isSlamming && !_isResetting)
        {
            if (Vector3.Distance(_slammerTarget.position, _player.position) < 2f)
            {
                _isSlamming = true;
                _waitCounter = _waitAfterSlam;
            }
        }

        if (_isSlamming)
        {
            Slam();
        }

        if (_isResetting)
        {
            ResetSlammer();
        }
    }

    private void Slam()
    {
        _slammer.position = Vector3.MoveTowards(_slammer.position, _slammerTarget.position, _slamSpeed * Time.deltaTime);
        
        if (_slammer.position == _slammerTarget.position)
        {
            _waitCounter -= Time.deltaTime;
            if (_waitCounter <= 0)
            {
                _isSlamming = false;
                _isResetting = true;
            }
        }
    }

    private void ResetSlammer()
    {
        _slammer.position = Vector3.MoveTowards(_slammer.position, _startPoint, _resetSpeed * Time.deltaTime);

        if (_slammer.position == _startPoint)
        {
            _isResetting = false;
        }
    }
}
