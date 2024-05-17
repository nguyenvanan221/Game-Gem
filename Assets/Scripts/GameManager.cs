using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private float score = 0;
    [SerializeField] TextMeshProUGUI scoreText;
    public CoinEffect coinEffect;
    public float Score
    {
        get { return score; }
        set
        {
            score = value;
            if (scoreText == null)
            {
                Debug.LogError("Score text is not set");
                return;
            }

            scoreText.text = string.Format("{0:0}", score);
        }
    }

    private void Start()
    {
        Score = 0;
    }

    private void Update()
    {
        if(SpawnGems.Instance.transform.childCount == 0)
        {
            SpawnGems.Instance.Initialize();
        }
    }
}
