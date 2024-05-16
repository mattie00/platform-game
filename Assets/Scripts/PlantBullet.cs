using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantBullet : MonoBehaviour
{
    [SerializeField] private float movementSpeed;
    [SerializeField] private string groundTag = "Ground";

    private void Update()
    {
        float moveValue = movementSpeed * Time.deltaTime;
        transform.position += transform.right * moveValue;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(groundTag))
        {
            Destroy(gameObject);
        }
        else if(collision.TryGetComponent(out Player player))
        {
            player.TakeDamage();
            Destroy(gameObject);
        }
    }
}
