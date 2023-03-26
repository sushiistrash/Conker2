using UnityEngine;

public class Bouncer : MonoBehaviour
{
    [SerializeField] private float _bounceForce = 20f;
    [SerializeField] private Animator _animator;

    public void Bounce(Player player)
    {
        if (player.TryGetComponent(out Rigidbody2D rigidbody))
        {
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, _bounceForce);
        }
        _animator.SetTrigger("bounce");
    }
}
