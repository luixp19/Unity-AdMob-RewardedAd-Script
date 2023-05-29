using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importa a biblioteca UnityEngine.UI, que cont�m classes relacionadas � cria��o de interfaces do usu�rio no Unity.
using TMPro; // Importa a biblioteca TMPro, que � uma biblioteca de texto avan�ada para o Unity.

public class UIConfig : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI diamondText; // Vari�vel que representa um elemento de texto na interface do usu�rio.

    private void Update()
    {
        // Atualiza o texto do elemento diamondText com o valor do diamante armazenado no banco de dados.
        diamondText.text = DiamondDataBase.diamond.ToString();
    }
}