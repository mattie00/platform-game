using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fruit : MonoBehaviour
{
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffectPrefab;
    [SerializeField] private float pickupEffectLifetime = 0.333f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerInventory playerInventory))
        {
            playerInventory.AddFruit(gameObject.tag);

            var spawnedPrefab = Instantiate(pickupEffectPrefab, transform.position, Quaternion.Euler(0, 0, Random.Range(0, 360)));
            Destroy(spawnedPrefab, pickupEffectLifetime);

            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
            Destroy(gameObject);  
        }
    }
}
