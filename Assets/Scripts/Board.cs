using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    public int width = 6;
    public int height = 6;

    public GameObject cellBackgroundPrefab_1;
    public GameObject cellBackgroundPrefab_2;

    //public GameObject[,] cellBackgrounds;
    public GameObject[,] allGems;

    void Start()
    {
        //cellBackgrounds = new GameObject[width, height];
        allGems = new GameObject[width, height];
        CreateMatrix();
    }

    //private void FixedUpdate()
    //{
    //    FindMatch(width, height);
    //}

    void CreateMatrix()
    {
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                GameObject cellBackground = (i + j) % 2 == 0 ? cellBackgroundPrefab_1 : cellBackgroundPrefab_2;

                Vector2 size = cellBackground.GetComponent<SpriteRenderer>().bounds.size;

                Vector2 position = new Vector2(i, j) * size;

                GameObject cellBackgroundObj = Instantiate(cellBackground, position, Quaternion.identity, this.transform);
                //cellBackgrounds[i, j] = cellBackgroundObj;
            }
        }
    }

    public void FindMatch(int column, int row)
    {
        
        if (allGems.Length != 0)
        {
            for (int i = 1; i < column - 2; i++)
            {
                for (int j = 1; j < row - 2; i++)
                {
                    GameObject currentGem = allGems[column, row];
                    GameObject rightGem = allGems[column + 1, row];
                    GameObject upGem = allGems[column, row + 1];
                    if (rightGem != null && currentGem != null)
                    {
                        if (rightGem.tag == currentGem.gameObject.tag)
                        {
                            rightGem.GetComponent<Gem>().isMatched = true;
                            currentGem.GetComponent<Gem>().isMatched = true;
                            Debug.Log("right bang nhau");
                            return;
                        }
                    }
                    if (upGem != null && currentGem != null) { 
                        if (upGem.tag == currentGem.gameObject.tag)
                        {
                            upGem.GetComponent<Gem>().isMatched = true;
                            currentGem.GetComponent<Gem>().isMatched = true;
                            Debug.Log("up bang nhau");
                            return;
                        }
                    }

                }
            }
        }
    }

}
