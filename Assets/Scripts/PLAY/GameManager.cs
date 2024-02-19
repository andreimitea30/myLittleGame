using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject platformPrefab;
    public int score = 0;
    public int platformCount = 3000;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 spawnPosition = new Vector3();
        for (int i = 0; i < platformCount; i++)
        {
            spawnPosition.y += Random.Range(2f, 5f);
            spawnPosition.x = Random.Range(-7f, 7f);
            score++;
            if(i < 5)
            {
                platformPrefab.transform.localScale = new Vector3(0.7f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            } else if (i < 10)
            {
                platformPrefab.transform.localScale = new Vector3(0.6f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            } else if (i < 15)
            {
                platformPrefab.transform.localScale = new Vector3(0.5f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            } else if (i < 20)
            {
                platformPrefab.transform.localScale = new Vector3(0.4f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            } else if (i < 25)
            {
                platformPrefab.transform.localScale = new Vector3(0.3f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            } else if (i < 30)
            {
                platformPrefab.transform.localScale = new Vector3(0.2f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            } else if (i < 35)
            {
                platformPrefab.transform.localScale = new Vector3(0.15f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            } else if (i < 40)
            {
                platformPrefab.transform.localScale = new Vector3(0.1f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            } else if (i < 450)
            {
                platformPrefab.transform.localScale = new Vector3(0.05f, 1f, 1f);
                Instantiate(platformPrefab, spawnPosition, Quaternion.identity);
                continue;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public int getScore()
{
    return score;
}
}


