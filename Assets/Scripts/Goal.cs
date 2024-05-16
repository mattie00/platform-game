using UnityEngine;

public class Goal : MonoBehaviour
{
    private GameUI gameUI;

    private void Start()
    {
        gameUI = FindObjectOfType<GameUI>();
        if (gameUI == null)
        {
            Debug.LogError("GameUI not found in the scene.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if (gameUI.AreAllFruitsCollected())
            {
                gameUI.TryDisplayLevelCompleteScreen();
            }
        }
    }
}
