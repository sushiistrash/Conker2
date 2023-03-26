using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerClimbing playerClimbing))
        {
            playerClimbing.IsOnLadder = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerClimbing playerClimbing))
        {
            playerClimbing.IsOnLadder = false;
        }
    }
}
