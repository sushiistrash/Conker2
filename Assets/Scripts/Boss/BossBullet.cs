using UnityEngine;

public class BossBullet : MonoBehaviour
{
    [SerializeField] private float _moveSpeed;
    [SerializeField] private int _damage = 1;

    private void Start()
    {
        AudioSystem.Instance.Play("Boss Shot");
    }
    
    private void Update()
    {
        transform.Translate(new Vector3(-_moveSpeed * transform.localScale.x * Time.deltaTime, 0, 0));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.DealDamage(_damage, name);
            AudioSystem.Instance.Play("Boss Impact");
        }

        Destroy(gameObject);
    }
}
