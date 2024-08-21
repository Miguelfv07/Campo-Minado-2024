using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Area[,] areas;

    [SerializeField] GameObject AreaPrefab;

    #region
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion

    const int diametroDoCampo = 5;

    private void Start()
    {
        GerarCampoMinado();
    }

    void GerarCampoMinado()
    {
        areas = new Area[diametroDoCampo, diametroDoCampo];

        for(int i = 0; i < diametroDoCampo; i++)
        {
            for(int j = 0; j < diametroDoCampo; j++)
            {
                Area area = Instantiate(AreaPrefab, new Vector2(i, j), Quaternion.identity).GetComponent<Area>();
                area.DefinirIndex(i,j);
                areas[i, j] = area;
            }
        }
    }

    public int ChecarEntorno(int x, int y)
    {
        int quantBombas = 0;
        for (int i = -1;i < 2 ;i++)
        {

            for(int j = -1;j < 2 ;j++)
            {
                if (x+i < diametroDoCampo && y+j < diametroDoCampo && x+i >= 0 && y+j >= 0)
                {
                    if (areas[x + i, y + j].Bomba)
                    {
                        quantBombas++;
                    }
                }
            }
        }
        return quantBombas;
    }
}
