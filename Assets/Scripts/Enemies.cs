using System.Collections;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    [SerializeField] private Transform leftPoint;
    [SerializeField] private Transform rightPoint;
    [SerializeField] private GameObject destroyParticlePrefab;
    [SerializeField] private float destroyParticleLifetime = 1.2f;
    [SerializeField] private EnemyDamageCollider damageCollider;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private float respawnTime = 30f;

    private Vector3 leftPointPosition;
    private Vector3 rightPointPosition;
    private bool isMovingRight = true;

    private void Start()
    {
        leftPointPosition = leftPoint.position;
        rightPointPosition = rightPoint.position;

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
        gameObject.SetActive(true);
    }

    private void Update()
    {
        if (!gameObject.activeSelf) return;

        float moveValue = movementSpeed * Time.deltaTime;

        if (isMovingRight)
        {
            transform.position = Vector3.MoveTowards(transform.position, rightPointPosition, moveValue);
            transform.rotation = Quaternion.Euler(0, 180, 0);

            if (transform.position == rightPointPosition)
            {
                isMovingRight = false;
            }
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, leftPointPosition, moveValue);
            transform.rotation = Quaternion.Euler(0, 0, 0);

            if (transform.position == leftPointPosition)
            {
                isMovingRight = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.TakeDamage();
        }
    }
}
