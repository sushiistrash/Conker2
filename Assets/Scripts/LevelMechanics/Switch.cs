using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Switch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite _switchDownSprite;
    [SerializeField] private Sprite _switchUpSprite;
    [SerializeField] private GameObject _objectToSwitch;
    [SerializeField] private bool _isSwitchedToDeactive;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isSwitchedToDeactive)
        {
            ActivateObject(other);
        }
        else
        {
            ActivateObject(other);
        }
    }

    private void ActivateObject(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _objectToSwitch.SetActive(_isSwitchedToDeactive);

            _spriteRenderer.sprite = _isSwitchedToDeactive ? _switchUpSprite : _switchDownSprite;
            _isSwitchedToDeactive = !_isSwitchedToDeactive;
        }
    }
}
