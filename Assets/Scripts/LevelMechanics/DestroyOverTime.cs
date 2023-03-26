using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    [SerializeField] private float _lifeTime = 0f;

    private void Update()
    {
        Destroy(gameObject, _lifeTime);
    }
}
