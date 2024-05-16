using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float movementSpeed = 1f;

    private bool IsMovingUp;
    private Vector3 startingPosition;

    private void Start()
    {
        startingPosition = transform.position;
    }

    private void Update()
    {
        float moveValue = movementSpeed * Time.deltaTime;
        if (IsMovingUp)
        {
            Vector3 targetPosition = startingPosition + new Vector3(0, maxY);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveValue);
            if (transform.position == targetPosition) 
            {
                IsMovingUp = false;
            }
        }
        else 
        {
            Vector3 targetPosition = startingPosition + new Vector3(0, minY);

            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveValue);
            if (transform.position == targetPosition)
            {
                IsMovingUp = true;
            }
        }
    }
}
