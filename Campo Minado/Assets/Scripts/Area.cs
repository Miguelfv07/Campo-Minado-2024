using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    bool bomba;
    bool revelado;
    int indexI, indexJ;

    [SerializeField] Sprite[] spritesVazios;

    public bool Bomba { get => bomba; }

    public void DefinirIndex(int i, int j)
    {
        indexI = i;
        indexJ = j;
    }

    public void Clicado()
    {
        Debug.Log("Clicado");
        if (!revelado)
        {
            if (bomba)
            {
                //GameOver
            }
            else
            {
                
                GetComponent<SpriteRenderer>().sprite = spritesVazios[GameManager.instance.ChecarEntorno(indexI, indexJ)];
            }
        }
    }
}
