using System.Collections;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void RespawnEnemy(GameObject enemy, Vector3 respawnPosition, float respawnTime)
    {
        StartCoroutine(RespawnCoroutine(enemy, respawnPosition, respawnTime));
    }

    private IEnumerator RespawnCoroutine(GameObject enemy, Vector3 respawnPosition, float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        enemy.transform.position = respawnPosition;
        enemy.SetActive(true);
    }
}
