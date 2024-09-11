using System.Collections;

using System.Collections.Generic;
using TMPro;
using UnityEngine;

using UnityEngine.UI;

public class ManagerUI : MonoBehaviour

{
    [SerializeField] Image barraDeDificuldade;

    [SerializeField] Gradient corDaBarra;

    [SerializeField] TextMeshProUGUI gameOvertext;

    public void AtualizarBarra(float value)

    {

        barraDeDificuldade.fillAmount = value;
        barraDeDificuldade.color = corDaBarra.Evaluate(value);

    }

    public void Atualizartexto(bool venceu)
    {
        if(venceu)
        {
            gameOvertext.text = "Vitória";
        }
        else
        {
            gameOvertext.text = "Derrota";
        }
    }

}

