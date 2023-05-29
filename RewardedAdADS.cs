using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;  // Importa o namespace GoogleMobileAds para usar as classes do SDK de an�ncios
using GoogleMobileAds.Api;  // Importa o namespace GoogleMobileAds.Api para usar as classes de API do SDK de an�ncios
using System;  // Importa o namespace System para usar tipos b�sicos do C#
using UnityEngine.UI;

public class RewardedAdADS : MonoBehaviour
{
    [Header ("Config")]

    public Button ShowButton;    // Refer�ncia ao bot�o para iniciar O anuncio
    public int numberDiamonds;    // Refer�ncia o a quantidade de diamantes a ser adicionada;
    RewardedAd rewardedAd;  // Vari�vel para armazenar o an�ncio recompensado carregado
    public string _adUnitId = "ca-app-pub-3940256099942544/5224354917";  // ID da unidade de an�ncios recompensados


    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        // Adiciona um ouvinte ao bot�o de carga da cena
        ShowButton.onClick.AddListener(ShowRewardedAd);

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadRewardedAd();
        });
    }

    // Carrega o an�ncio recompensado
    [Obsolete]
    public void LoadRewardedAd()
    {
        // Limpa o an�ncio antigo antes de carregar um novo
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // Cria a solicita��o usada para carregar o an�ncio
        var adRequest = new AdRequest.Builder().Build();

        // Envia a solicita��o para carregar o an�ncio recompensado
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // Se o erro n�o for nulo, a solicita��o de carregamento falhou
                if (error != null || ad == null)
                {
                    Debug.LogError("Rewarded ad failed to load an ad " +
                                   "with error: " + error.GetMessage());
                    return;
                }

                Debug.Log("Rewarded ad loaded with response: "
                          + ad.GetResponseInfo());

                rewardedAd = ad;
                RegisterEventHandlers(rewardedAd);
            });
    }

    // Mostra o an�ncio recompensado
    public void ShowRewardedAd()
    {

        // Verifica se o an�ncio est� carregado e pode ser exibido
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Recompense o usu�rio
                DiamondDataBase.diamond = DiamondDataBase.diamond + numberDiamonds; // Busca a variavel "diamond" existente dentro do script "DiamondDataBase" e adiciona um valora ela
                DiamondDataBase.diamondCurrentCondition = "Save"; // salva e atualiza o valor da variavel "diamond" existente dentro do script "DiamondDataBase"
            });
        }
    }

    // Registra os manipuladores de eventos para o an�ncio recompensado
    [Obsolete]
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Evento disparado quando o an�ncio � estimado para ter gerado dinheiro
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };

        // Evento disparado quando uma impress�o � registrada para um an�ncio
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };

        // Evento disparado quando um clique � registrado para um an�ncio
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };

        // Evento disparado quando o conte�do em tela cheia do an�ncio � aberto
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };

        // Evento disparado quando o conte�do em tela cheia do an�ncio � fechado
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();
        };

        // Evento disparado quando falha ao abrir o conte�do em tela cheia do an�ncio
        ad.OnAdFullScreenContentFailed += (AdError error) =>
        {
            Debug.LogError("Rewarded ad failed to open full screen content " +
                           "with error: " + error);
            LoadRewardedAd();
        };
    }

    // Update is called once per frame
    void Update()
    {
        // Este m�todo � chamado a cada quadro (frame) do jogo, mas est� vazio neste exemplo
    }
}

