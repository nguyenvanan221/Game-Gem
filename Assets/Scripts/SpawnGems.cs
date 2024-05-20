using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGems : Singleton<SpawnGems>
{
    public GameObject[] gemsPrefab;
    //public int numberGems;

    void Start()
    {
        Initialize();
        //numberGems = 0;
    }

    public void Initialize()
    {
        for (int i = 0; i < gemsPrefab.Length; i++)
        {
            Instantiate(gemsPrefab[i], randomPosition(), Quaternion.identity, transform);
            Instantiate(gemsPrefab[i], randomPosition(), Quaternion.identity, transform);
        }

    }

    Vector3 randomPosition()
    {
        Vector3 randomPos = Vector3.zero;
        float cameraWidth = Camera.main.orthographicSize * 2f * Camera.main.aspect;
        float cameraHeight = Camera.main.orthographicSize * 2f;

        bool findEmptySpace = false;

        while (!findEmptySpace)
        {
            randomPos = new Vector3(
                Random.Range(-0.2f, (cameraWidth / 2f) + 2.0f),
                Random.Range(-3.5f, (-cameraWidth / 2f) + 2.5f),
                0f
            );

            Collider2D[] colliders = Physics2D.OverlapCircleAll(randomPos, 0.2f);
            if (colliders.Length == 0)
            {
                findEmptySpace = true;
            }
        }
        return randomPos;
    }
}
