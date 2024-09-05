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

     int diametroDoCampo ;
     int numeroDeBombas ;

    ManagerUI managerUI;
    GameObject menu, gameOver;


    private void Start()
    {
         managerUI = GetComponent<ManagerUI>();
        menu = GameObject.Find("Menu");
        gameOver = GameObject.Find("GameOver");
    }

    public void DefinirDiametro(string value)
    {
        diametroDoCampo = int.Parse(value);
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo));
    }

    public void DefinirBombas(string value)
    {
        numeroDeBombas = int.Parse(value);
        managerUI.AtualizarBarra((float)numeroDeBombas / (diametroDoCampo * diametroDoCampo));
    }

    public void IniciarJogo()
    {
        ExcluirCampo();
        GerarCampoMinado();
        DistribuirBombas();
        menu.SetActive(false);
        gameOver.SetActive(false);
    }

    public void ExcluirCampo()
    {
        if (areas != null)
        {
            foreach (Area area in areas)
            {
                Destroy(area.gameObject);
            }
        }

    }

   public void GerarCampoMinado()
    {
        if (numeroDeBombas < diametroDoCampo * diametroDoCampo)
        {

            areas = new Area[diametroDoCampo, diametroDoCampo];

            for (int i = 0; i < diametroDoCampo; i++)
            {
                for (int j = 0; j < diametroDoCampo; j++)
                {
                    Area area = Instantiate(AreaPrefab, new Vector2(i, j), Quaternion.identity).GetComponent<Area>();
                    area.DefinirIndex(i, j);
                    areas[i, j] = area;
                }
            }
            //Organizar a camera para ficar no meio do diametro
            Camera.main.transform.position = new Vector3(diametroDoCampo / 2f - 0.5f, diametroDoCampo / 2f - 0.5f, -10);

            //Organizar a camera na altura do campo (matriz)
            Camera.main.orthographicSize = diametroDoCampo / 2f;
           
        }
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

        foreach(Area area in areas)
        {
            if (area.Bomba)
            {
                area.RevelarBomba();            
            }

        }

        gameOver.SetActive(true);

    }
}
