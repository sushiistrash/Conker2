using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Checkpoint : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _checkpointOffSprite, _checkpointOnSprite;
    private CheckpointController _checkpointController;
    private ActivationState _activationState;

    private void Start()
    {
        if (transform.parent != null)
        {
            if (transform.parent.TryGetComponent(out CheckpointController checkpointController))
            {
                _checkpointController = checkpointController;
            }
        }
        _activationState = _spriteRenderer.sprite == _checkpointOffSprite ? ActivationState.Off : ActivationState.On;
        if (IsActive())
        {
            SetSpawnPosition(transform.position);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            Debug.Log("Player has reached the checkpoint");
            _checkpointController.ResetAllCheckpoints();
            ActivateCheckpoint();
            SetSpawnPosition(transform.position);
        }
    }

    private void SetSpawnPosition(Vector3 position)
    {
        _checkpointController.SpawnPosition = position;
    }

    private void ActivateCheckpoint()
    {
        _spriteRenderer.sprite = _checkpointOnSprite;
        _activationState = ActivationState.On;
    }

    public void ResetCheckpoint()
    {
        _spriteRenderer.sprite = _checkpointOffSprite;
        _activationState = ActivationState.Off;
    }
    
    public bool IsActive() => _activationState == ActivationState.On;

    private enum ActivationState
    {
        Off,
        On,
    }
}
