using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D playerRigidbody;
    [SerializeField] private GroundChecker groundChecker;
    [SerializeField] private Transform legsTransform;
    [SerializeField] private GameObject landingParticlePrefab;
    [SerializeField] private float landingParticleLifetime = 1f;
    [SerializeField] private GameObject movementParticlePrefab;
    [SerializeField] private float movementParticleLifetime = 0.3f;



    [Space(5)]
    [Header("Settings")]
    [Range(300, 800)]
    [SerializeField] private float moveSpeed = 5f;
    [Range(3, 12)]
    [SerializeField] private float jumpPower = 4f;
    [SerializeField] private float doubleJumpPower = 2.5f;

    [Space(5)]
    [Header("Sounds")]
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float moveSoundDelay = 0.3f;
    [SerializeField] private AudioClip moveSound;

    private float moveSoundTimer = 0f;
    private float inputX;
    private bool isJumpingInput = false;
    private bool isDoubleJump = false;
    private Platform currentPlatform;

    private void Start()
    {
        groundChecker.Onlanding += HandleLanding;
    }

    private void HandleLanding()
    {
        var spawnedPrefab = Instantiate(landingParticlePrefab, legsTransform.position, Quaternion.identity);
        Destroy(spawnedPrefab, landingParticleLifetime);
    }

    private void Update()
    {
        HandleInput();
        HandleMovementEffects();
    }

    private void HandleMovementEffects()
    {
        if (IsMoving() && groundChecker.IsGrounded)
        {
            moveSoundTimer += Time.deltaTime;
            if (moveSoundTimer > moveSoundDelay)
            {
                moveSoundTimer -= moveSoundDelay;
                AudioSource.PlayClipAtPoint(moveSound, transform.position);
                var spawnedPrefab = Instantiate(movementParticlePrefab, legsTransform.position, Quaternion.identity);
                Destroy(spawnedPrefab, movementParticleLifetime);
            }
        }
    }

    private void HandleInput()
    {
        inputX = Input.GetAxis("Horizontal");

        if (groundChecker.IsGrounded)
        {
            isDoubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundChecker.IsGrounded) 
            {
                isDoubleJump = true;
                isJumpingInput = true;
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);

            }
            else if (isDoubleJump)
            {
                isJumpingInput = true;
                isDoubleJump = false;
                AudioSource.PlayClipAtPoint(jumpSound, transform.position);
            }
        }

        if (Input.GetKeyDown(KeyCode.S) && currentPlatform != null && groundChecker.IsGrounded)
        {
            currentPlatform.SetCollidable(false);
        }
    }

    private void FixedUpdate()
    {
        float moveInput = inputX * Time.fixedDeltaTime * moveSpeed;
        playerRigidbody.velocity = new Vector2(moveInput, playerRigidbody.velocity.y);

        if (isJumpingInput )
        {
            float currentJumpPower = jumpPower;
            if (!isDoubleJump)
            {
                currentJumpPower = doubleJumpPower;
            }

            playerRigidbody.velocity = new Vector2(playerRigidbody.velocity.x, 0);
            playerRigidbody.AddForce(new Vector2(0, jumpPower), ForceMode2D.Impulse);
            isJumpingInput = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            currentPlatform = platform;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.TryGetComponent(out Platform platform))
        {
            currentPlatform = null;
        }
    }

    public bool IsMoving()
    {
        return inputX != 0;
    }

    public float GetCurrentInputX()
    { 
        return inputX;
    }
}
