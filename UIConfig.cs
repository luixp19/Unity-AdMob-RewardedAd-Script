using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // Importa a biblioteca UnityEngine.UI, que contém classes relacionadas à criação de interfaces do usuário no Unity.
using TMPro; // Importa a biblioteca TMPro, que é uma biblioteca de texto avançada para o Unity.

public class UIConfig : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] private TextMeshProUGUI diamondText; // Variável que representa um elemento de texto na interface do usuário.

    private void Update()
    {
        // Atualiza o texto do elemento diamondText com o valor do diamante armazenado no banco de dados.
        diamondText.text = DiamondDataBase.diamond.ToString();
    }
}