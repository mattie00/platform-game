using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject destroyParticlePrefab;
    [SerializeField] private float destroyParticleLifetime = 1.2f;
    [SerializeField] private EnemyDamageCollider damageCollider;
    [SerializeField] private AudioClip damageSound;

    [Header("Shooting")]
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject plantBulletPrefab;
    [SerializeField] private AudioClip shootSound;
    [SerializeField] private float shootDelay = 1f;

    [Header("Animations")]
    [SerializeField] private Animator animator;
    [SerializeField] private string shootTrigger;

    [Header("Respawn")]
    [SerializeField] private float respawnTime = 45f;

    private float shootTimer = 0f;

    private void Start()
    {
        damageCollider.OnPlayerJumped += TakeDamage;
    }

    private void TakeDamage()
    {
        AudioSource.PlayClipAtPoint(damageSound, transform.position);

        var spawnedPrefab = Instantiate(destroyParticlePrefab, transform.position, Quaternion.identity);
        Destroy(spawnedPrefab, destroyParticleLifetime);

        gameObject.SetActive(false);

        Invoke(nameof(Respawn), respawnTime);
    }

    private void Respawn()
    {
        shootTimer = 0f;

        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        shootTimer += Time.deltaTime;
        if (shootTimer >= shootDelay)
        {
            shootTimer -= shootDelay;
            Shoot();
        }
    }

    private void Shoot()
    {
        var spawnedBullet = Instantiate(plantBulletPrefab, shootPoint.position, Quaternion.identity);
        spawnedBullet.transform.right = -transform.right;

        animator.SetTrigger(shootTrigger);
        AudioSource.PlayClipAtPoint(shootSound, transform.position);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage();
        }
    }
}
