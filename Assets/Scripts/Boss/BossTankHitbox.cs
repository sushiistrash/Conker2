using UnityEngine;

public class BossTankHitbox : MonoBehaviour
{
    [SerializeField] private BossTank _bossTank;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out Player player) && player.transform.position.y > transform.position.y)
        {
            _bossTank.TakeHit();
            player.Bounce();
            gameObject.SetActive(false);
        }
    }
}
