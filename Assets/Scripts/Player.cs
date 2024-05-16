using System;
using System.Net.Sockets;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private AudioClip damageSound;
    [SerializeField] private AudioClip gameOverSound;
    [SerializeField] private Rigidbody2D playerRigidbody;

    public event Action OnLose;
    public int lives = 3;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position; 
    }

    public void TakeDamage()
    {
        lives--;
        AudioSource.PlayClipAtPoint(damageSound, transform.position);

        if (lives > 0)
        {
            ResetPosition();
        }
        else
        {
            OnLose?.Invoke();
            AudioSource.PlayClipAtPoint(gameOverSound, transform.position);
            gameObject.SetActive(false);
        }
    }

    public void AddForce(float force)
    {
        playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
        playerRigidbody.AddForce(new Vector2(0, force), ForceMode2D.Impulse);
    }

    private void ResetPosition()
    {
        transform.position = startPosition;
    }
}
