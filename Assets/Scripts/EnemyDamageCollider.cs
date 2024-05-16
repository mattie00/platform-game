using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageCollider : MonoBehaviour
{
    [SerializeField] private float playerJumpPower = 12f;
    public event Action OnPlayerJumped;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.AddForce(playerJumpPower);
            OnPlayerJumped?.Invoke();
        }
    }
}
