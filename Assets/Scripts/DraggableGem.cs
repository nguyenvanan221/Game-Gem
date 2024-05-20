using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class DraggableGem : MonoBehaviour
{
    Vector3 dragOffset;
    new Collider2D collider;
    public string cellBackgroundTag = "CellBackground";
 
    private Board board;
    private Gem gem;

    Vector2 size; 
    Vector3 posBeforeDrag;
    Vector2Int posBeforeInArray = new Vector2Int(-1, -1);

    bool findMatch;

    void Awake()
    {
        collider = GetComponent<Collider2D>();
        board = FindObjectOfType<Board>();
        gem = transform.GetComponent<Gem>();
        size = transform.GetComponent<SpriteRenderer>().bounds.size;
    }

    private void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePos();
        posBeforeDrag = transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMousePos() + dragOffset;
    }

    private void OnMouseUp()
    {
        collider.enabled = false;

        var rayOrigin = transform.position;
        var rayDirection = Vector3.forward;
        RaycastHit2D hitInfo;
        if (hitInfo = Physics2D.Raycast(rayOrigin, rayDirection))
        {
            
            if (hitInfo.transform.CompareTag(cellBackgroundTag))
            {
                transform.position = hitInfo.transform.position;
                

                var i = (int)Mathf.Round(transform.position.x / size.x);
                var j = (int)Mathf.Round(transform.position.y / size.y);
                
                Vector2Int posCurrentInArray = new Vector2Int(i, j);

                if (board.allGems[i, j] == null)
                {
                    board.allGems[i, j] = gameObject;
                    if (posBeforeInArray != new Vector2Int(-1, -1))
                    {
                        board.allGems[posBeforeInArray.x, posBeforeInArray.y] = null;
                    }
                    FindMatch(i, j, transform.gameObject.tag);
                    
                }
                else
                {
                    transform.position = posBeforeDrag;
                }
                posBeforeInArray = posCurrentInArray;

            }
            else
            {
                transform.position = posBeforeDrag;
            }
        }
        else
        {
            transform.position = posBeforeDrag;
        }
        
        collider.enabled = true;
    }

    Vector3 GetMousePos()
    {
        var mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public void FindMatch(int column, int row, string tag)
    {
        
        GameObject rightGem = (column + 1 < board.width) ? board.allGems[column + 1, row] : null;
        GameObject leftGem = (column - 1 >= 0) ? board.allGems[column - 1, row] : null;
        GameObject upGem = (row + 1 < board.height) ? board.allGems[column, row + 1] : null;
        GameObject downGem = (row - 1 >= 0) ? board.allGems[column, row - 1] : null;

        if (rightGem != null && rightGem.tag == tag)
        {
            rightGem.GetComponent<Gem>().isMatched = true;
            transform.GetComponent<Gem>().isMatched = true;
            CoinEffect((rightGem.transform.position + transform.position) /2f);
            
        }

        if (leftGem != null && leftGem.tag == tag)
        {
            leftGem.GetComponent<Gem>().isMatched = true;
            transform.GetComponent<Gem>().isMatched = true;
            CoinEffect((leftGem.transform.position + transform.position) / 2f);

        }

        if (upGem != null && upGem.tag == tag)
        {
            upGem.GetComponent<Gem>().isMatched = true;
            transform.GetComponent<Gem>().isMatched = true;
            CoinEffect((upGem.transform.position + transform.position) / 2f);
        }

        if (downGem != null && downGem.tag == tag)
        {
            downGem.GetComponent<Gem>().isMatched = true;
            transform.GetComponent<Gem>().isMatched = true;
            CoinEffect((downGem.transform.position + transform.position) / 2f);
        }

    }

    void CoinEffect(Vector3 position)
    {
        GameManager.Instance.Score += 10;
        GameManager.Instance.coinEffect.StartCoinEffect(position);
    }

}
