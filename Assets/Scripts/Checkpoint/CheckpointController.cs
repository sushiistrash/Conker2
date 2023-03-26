using System.Collections;
using UnityEngine;

public class CheckpointController : MonoBehaviour
{
    [HideInInspector] public Vector3 SpawnPosition;
    [SerializeField] private float _waitForRespawn;
    [SerializeField] private GameObject Player;

    public void RespawnPlayer()
    {
        StartCoroutine(RespawnCoroutine());
        Debug.Log($"Player respawning with {PlayerHealth.MaxHealth} health!");
    }

    private IEnumerator RespawnCoroutine()
    {
        Player.SetActive(false);

        yield return new WaitForSeconds(_waitForRespawn - (1f / UISystem.Instance.FadeSpeed));
        UISystem.Instance.FadeToBlack();
        yield return new WaitForSeconds(1f / UISystem.Instance.FadeSpeed);;
        UISystem.Instance.FadeFromBlack();

        Player.SetActive(true);
        Player.transform.position = SpawnPosition;
        if (Player.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.CurrentHealth = PlayerHealth.MaxHealth;
            UISystem.Instance.UpdateHealthDisplay(playerHealth.CurrentHealth);
        }
    }

    public void ResetAllCheckpoints()
    {
        Checkpoint[] checkpoints = GetComponentsInChildren<Checkpoint>();
        foreach (Checkpoint checkpoint in checkpoints)
        {
            if (checkpoint.IsActive())
            {
                checkpoint.ResetCheckpoint();
            }
        }
    }
}
