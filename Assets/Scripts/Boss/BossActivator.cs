using UnityEngine;

public class BossActivator : MonoBehaviour
{
    [SerializeField] private GameObject _bossBattle;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Player>(out _))
        {
            _bossBattle.SetActive(true);

            gameObject.SetActive(false);
            AudioSystem.Instance.Play("Boss Battle");
        }
    }
}
