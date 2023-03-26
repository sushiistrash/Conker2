using UnityEngine;

public class ParallaxBehaviour : MonoBehaviour
{
    [SerializeField] private Transform _followingTargetTransform;
    [SerializeField, Range(0f, 1f)] private float _parallaxStrength;
    [SerializeField] private bool _disableVerticalParallax;
    private Vector3 _targetPreviousPosition;

    private void Start()
    {
        if (!_followingTargetTransform)
            _followingTargetTransform = Camera.main.transform;

        _targetPreviousPosition = _followingTargetTransform.position;
    }

    private void Update()
    {
        Vector3 amountToMove = _followingTargetTransform.position - _targetPreviousPosition;

        if (_disableVerticalParallax)
            amountToMove.y = 0;

        transform.position += amountToMove * _parallaxStrength;

        _targetPreviousPosition = _followingTargetTransform.position;
    }
}
