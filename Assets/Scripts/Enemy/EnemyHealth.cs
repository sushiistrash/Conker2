using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private GameObject _item;
    [SerializeField, Range(0f, 100f)] private float _chanceToDropItem;

    [SerializeField] private GameObject _deathEffect;

    [SerializeField] private int _maxHealth;
    private int _currentHealth;
    public int CurrentHealth
    {
        get { return _currentHealth; }
        set
        {
            if (value < 0 || value > _maxHealth)
                Debug.LogWarning($"The value {value} must be greater than or equal to 0 and less or equal than {_maxHealth}");
            _currentHealth = value;
        }
    }

    private void Awake()
    {
        _currentHealth = _maxHealth;
    }

    public void DealDamage(int damage)
    {
        Debug.Log($"Enemy takes {damage} damage");

        _currentHealth -= damage;
        CheckAlive(_currentHealth);
    }

    public void DropItem()
    {
        float dropSelect = Random.Range(0f, 100f);

        if (dropSelect <= _chanceToDropItem)
        {
            Instantiate(_item, transform.position, Quaternion.identity);
        }
    }

    private void CheckAlive(int currentHealth)
    {
        if (currentHealth <= 0)
        {
            Debug.Log("Enemy has just died");
            DropItem();
            Instantiate(_deathEffect, transform.position, Quaternion.identity);
            transform.gameObject.SetActive(false);
        }
    }
}
