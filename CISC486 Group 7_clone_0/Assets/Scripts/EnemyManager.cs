using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity);
    }
}
