using UnityEngine;

public class FeetBox : MonoBehaviour
{
    [SerializeField] private int _amountOfDamage;

    private Player _player;

    private void Awake()
    {
        if (transform.parent == null) return;
        if (transform.parent.TryGetComponent(out Player player))
        {
            _player = player;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out EnemyHealth enemyHealth))
        {
            Debug.Log($"Player hitting {other.transform.name}");

            AudioSystem.Instance.Play("Enemy Explode");

            enemyHealth.DealDamage(_amountOfDamage);
            _player.Bounce();
        }

        if (other.TryGetComponent(out Bouncer bouncer))
        {
            bouncer.Bounce(_player);
        }

        if (other.transform.parent == null) return;
        if (other.transform.parent.TryGetComponent<MovingPlatform>(out _))
        {
            _player.transform.parent = other.transform;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.parent == null) return;
        if (other.transform.parent.TryGetComponent<MovingPlatform>(out _))
        {
            _player.transform.parent = null;
        }
    }
}
 