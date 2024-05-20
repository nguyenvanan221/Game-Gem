using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinEffect : MonoBehaviour
{
    public Transform coinPrefab;
    public float bounceHeight = 1.0f; 
    public float bounceTime = 1.0f;  
    public float rotationSpeed = 360.0f;


    public void StartCoinEffect(Vector3 startPosition)
    {
        StartCoroutine(CoinBounceAndRotate(startPosition));
    }

    private IEnumerator CoinBounceAndRotate(Vector3 startPosition)
    {
        Transform coin = Instantiate(coinPrefab, startPosition, Quaternion.identity);

        Vector3 endPosition = startPosition + new Vector3(0, bounceHeight, 0);

        float elapsedTime = 0;
        while (elapsedTime < bounceTime)
        {
            float t = elapsedTime / bounceTime;

            coin.position = Vector3.Lerp(startPosition, endPosition, t);

            coin.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        coin.position = endPosition;
        Destroy(coin.gameObject);
    }
}
