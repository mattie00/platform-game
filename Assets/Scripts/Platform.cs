using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField] private BoxCollider2D boxCollider;

    private bool isCollidable;

    public void SetCollidable(bool isCollidable)
    {
        this.isCollidable = isCollidable;
        boxCollider.enabled = isCollidable;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player)) 
        {
            SetCollidable(true);
        }
    }
}
