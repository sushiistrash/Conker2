using UnityEngine;

public class BossTankMine : MonoBehaviour
{
    [SerializeField] private GameObject _explosion;
    [SerializeField] private int _damage = 1;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            Destroy(gameObject);
            Instantiate(_explosion, transform.position, Quaternion.identity);

            playerHealth.DealDamage(_damage, name);

            AudioSystem.Instance.Play("Enemy Explode");
        }
    }

    public void Explode()
    {
        Destroy(gameObject);

        Instantiate(_explosion, transform.position, transform.rotation);
    }
}
