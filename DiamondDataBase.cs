using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiamondDataBase : MonoBehaviour
{
    [Header("Config")]
    public static int diamond; // Quantidade de diamantes (variável estática)
    public static string diamondCurrentCondition; // Condição atual dos diamantes (variável estática)

    private void Start()
    {
        UpdateDiamond(); // Atualiza o valor dos diamantes ao iniciar
    }

    private void Update()
    {
        // Verifica se a condição atual dos diamantes é "Save"
        if (diamondCurrentCondition == "Save")
        {
            SaveDiamond(); // Salva os diamantes
            UpdateDiamond(); // Atualiza o valor dos diamantes
            diamondCurrentCondition = "Null"; // Reseta a condição atual dos diamantes para "Null"
        }
    }

    public void UpdateDiamond()
    {
        diamond = PlayerPrefs.GetInt("diamond"); // Obtém o valor dos diamantes do PlayerPrefs
    }

    public void SaveDiamond()
    {
        PlayerPrefs.SetInt("diamond", diamond); // Salva o valor dos diamantes no PlayerPrefs
    }
}