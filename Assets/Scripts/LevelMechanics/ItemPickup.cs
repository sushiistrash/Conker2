using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private bool _isCollected = false;
    private const int _amountofHeal = 1;

    [SerializeField] private PickupType _pickupType;

    [SerializeField] private GameObject _pickupEffect;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isCollected || !other.TryGetComponent<Player>(out _))
        {
            return;
        }
        if (_pickupType == PickupType.Gem)
        {
            if (other.TryGetComponent(out PlayerItems playerItems))
            {
                playerItems.GemsCollected++;
                SaveSystem.Instance.ActiveSave.Gems = playerItems.GemsCollected;
                SaveSystem.Instance.Save();
                AudioSystem.Instance.Play("Pickup Gem");
                UISystem.Instance.UpdateGemCount(playerItems.GemsCollected);
            }
        }
        else if (_pickupType == PickupType.Heal)
        {
            if (!(other.TryGetComponent(out PlayerHealth playerHealth) && playerHealth.HealPlayer(_amountofHeal)))
            {
                return;
            }
        }
        Debug.Log($"Player picking up {gameObject.name}");
        _isCollected = true;
        Destroy(gameObject);
        Instantiate(_pickupEffect, transform.position, Quaternion.identity);
    }

    private enum PickupType
    {
        Gem,
        Heal,
    }
}
