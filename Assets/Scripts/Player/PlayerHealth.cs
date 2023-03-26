using UnityEngine;
using System;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerHealth : MonoBehaviour
{
    public const int MaxHealth = 3;
    private int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value < 0 || value > MaxHealth) Debug.LogWarning($"The value {value} must be greater than or equal to 0 and less or equal than {MaxHealth}");
            else _currentHealth = value;
        }
    }

    [SerializeField] private float _invincibleDuration = 1;
    private float _invincibleCounter = 0;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    [SerializeField] private GameObject _deathEffect;
    [SerializeField] private CheckpointController _checkpointController;

    private void Awake()
    {
        _currentHealth = MaxHealth;
    }
    
    private void Update()
    {

        if (_invincibleCounter > 0)
        {
            _invincibleCounter -= Time.deltaTime;

            if (_invincibleCounter <= 0)
            {
                _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, 1f);
            }
        }
    }

    public bool HealPlayer(int amountOfHeal)
    {
        if (_currentHealth >= MaxHealth)
        {
            Debug.Log("Player can't pick up health since he has max health points");
            return false;
        }
        _currentHealth += amountOfHeal;
        UISystem.Instance.UpdateHealthDisplay(_currentHealth);
        AudioSystem.Instance.Play("Pickup Health");
        return true;
    }

    public void DealDamage(int damage, string name)
    {
        Debug.Log($"Player takes {damage} amount of damage from the {name}");
        if (_invincibleCounter <= 0)
        {
            _currentHealth -= damage;
            CheckAlive();
            UISystem.Instance.UpdateHealthDisplay(_currentHealth);
        }
    }

    private void CheckAlive()
    {
        if (_currentHealth <= 0)
        {
            Debug.Log("Player has just died");
            AudioSystem.Instance.Play("Player Death");
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            Respawn();
        }
        else
        {
            _invincibleCounter = _invincibleDuration;
            _spriteRenderer.color = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, .5f);
            if (TryGetComponent(out Player player))
            {
                player.KnockBack();
            }
        }
    }

    public void Respawn()
    {
        _checkpointController.RespawnPlayer();
    }
}
