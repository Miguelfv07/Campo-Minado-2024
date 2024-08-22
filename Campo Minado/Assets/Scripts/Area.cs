using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    [SerializeField] bool bomba;
    bool revelado;

    int indexI, indexJ;



    [SerializeField] Sprite[] spritesVazios;
    [SerializeField] Sprite bombaSprite;

    public bool Bomba { get => bomba; set => bomba = value; }

    public void DefinirIndex(int i, int j)
    {
        indexI = i;
        indexJ = j;
    }

    public void Revelar()
    {
        Debug.Log("Clicado");
        if (!revelado)
        {
            if (bomba)
            {
                GameManager.instance.GameOver();
            }
            else
            {
                revelado = true;
                GetComponent<SpriteRenderer>().sprite = spritesVazios[GameManager.instance.ChecarEntorno(indexI, indexJ)];
            }
        }
    }

    public void RevelarBomba()
    {
        revelado = true;
        GetComponent<SpriteRenderer>().sprite = bombaSprite;
    }
}
