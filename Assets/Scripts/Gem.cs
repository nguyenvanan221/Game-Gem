using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : MonoBehaviour
{
    public bool isMatched;

    private void Update()
    {
        if (isMatched)
        {
            
            Destroy(gameObject);
        }
    }

}
