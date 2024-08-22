using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    Area[,] areas;

    [SerializeField] GameObject AreaPrefab;

    const int diametroDoCampo = 5;
    const int numeroDeBombas = 10;

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
                area.DefinirIndex(i, j);
                areas[i, j] = area;
            }
        }

        DistribuirBombas();
    }

    public int ChecarEntorno(int x, int y)
    {
        int quantidadeDeBombas = 0;

        for (int i = -1;i < 2;i++)
        {
            for(int j = -1;j < 2; j++)
            {
                if (x+i < diametroDoCampo && y+j < diametroDoCampo && x+i >= 0 && y+j >= 0) 
                {
                    if (areas[x + i, y + j].Bomba)
                    {
                        quantidadeDeBombas++;
                    } 
                }
            }
        }

        if(quantidadeDeBombas == 0)
        {
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    if (x + i < diametroDoCampo && y + j < diametroDoCampo && x + i >= 0 && y + j >= 0)
                    {
                        areas[x + i, y + j].Revelar();
                    }
                }
            }
        }

        return quantidadeDeBombas;
    }

    void DistribuirBombas()
    {
        int quantidadeDeBombas = 0;

        while (quantidadeDeBombas < numeroDeBombas)
        {
            int[] index = new int[2];

            index[0] = Random.Range(0, diametroDoCampo);
            index[1] = Random.Range(0, diametroDoCampo);

            if (areas[index[0], index[1]].Bomba == false) 
            {
                areas[index[0], index[1]].Bomba = true;
                quantidadeDeBombas++; 
            }
        }
    }

    public void GameOver()
    {
        for(int i = 0; i < diametroDoCampo; i++) 
        {
            for (int j = 0; j < diametroDoCampo; j++)
            {
                if (areas[i, j].Bomba)
                {
                    areas[i, j].RevelarBomba();
                }
            }
        }
    }
}
