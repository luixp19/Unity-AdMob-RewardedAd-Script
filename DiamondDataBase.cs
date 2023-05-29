using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDataBase : MonoBehaviour
{
    [Header("Config")]
    public static int diamond; // Quantidade de diamantes (vari�vel est�tica)
    public static string diamondCurrentCondition; // Condi��o atual dos diamantes (vari�vel est�tica)

    private void Start()
    {
        UpdateDiamond(); // Atualiza o valor dos diamantes ao iniciar
    }

    private void Update()
    {
        // Verifica se a condi��o atual dos diamantes � "Save"
        if (diamondCurrentCondition == "Save")
        {
            SaveDiamond(); // Salva os diamantes
            UpdateDiamond(); // Atualiza o valor dos diamantes
            diamondCurrentCondition = "Null"; // Reseta a condi��o atual dos diamantes para "Null"
        }
    }

    public void UpdateDiamond()
    {
        diamond = PlayerPrefs.GetInt("diamond"); // Obt�m o valor dos diamantes do PlayerPrefs
    }

    public void SaveDiamond()
    {
        PlayerPrefs.SetInt("diamond", diamond); // Salva o valor dos diamantes no PlayerPrefs
    }
}