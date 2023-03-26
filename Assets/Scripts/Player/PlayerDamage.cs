using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] private int _amountOfDamage = 1;
    public EnemyType Enemy;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            if (Enemy == EnemyType.DeathArea)
            {
                playerHealth.CurrentHealth = 0;
                playerHealth.Respawn();
                return;
            }
            playerHealth.DealDamage(_amountOfDamage, Enemy.ToString());
        }
    }

    public enum EnemyType
    {
        Spike,
        DeathArea,
        Frog,
        Eagle,
        Slammer,
    }
}
