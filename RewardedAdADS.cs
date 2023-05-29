using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;  // Importa o namespace GoogleMobileAds para usar as classes do SDK de anúncios
using GoogleMobileAds.Api;  // Importa o namespace GoogleMobileAds.Api para usar as classes de API do SDK de anúncios
using System;  // Importa o namespace System para usar tipos básicos do C#
using UnityEngine.UI;

public class RewardedAdADS : MonoBehaviour
{
    [Header ("Config")]

    public Button ShowButton;    // Referência ao botão para iniciar O anuncio
    public int numberDiamonds;    // Referência o a quantidade de diamantes a ser adicionada;
    RewardedAd rewardedAd;  // Variável para armazenar o anúncio recompensado carregado
    public string _adUnitId = "ca-app-pub-3940256099942544/5224354917";  // ID da unidade de anúncios recompensados


    // Start is called before the first frame update
    [Obsolete]
    void Start()
    {
        // Adiciona um ouvinte ao botão de carga da cena
        ShowButton.onClick.AddListener(ShowRewardedAd);

        MobileAds.Initialize((InitializationStatus initStatus) =>
        {
            LoadRewardedAd();
        });
    }

    // Carrega o anúncio recompensado
    [Obsolete]
    public void LoadRewardedAd()
    {
        // Limpa o anúncio antigo antes de carregar um novo
        if (rewardedAd != null)
        {
            rewardedAd.Destroy();
            rewardedAd = null;
        }

        Debug.Log("Loading the rewarded ad.");

        // Cria a solicitação usada para carregar o anúncio
        var adRequest = new AdRequest.Builder().Build();

        // Envia a solicitação para carregar o anúncio recompensado
        RewardedAd.Load(_adUnitId, adRequest,
            (RewardedAd ad, LoadAdError error) =>
            {
                // Se o erro não for nulo, a solicitação de carregamento falhou
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

    // Mostra o anúncio recompensado
    public void ShowRewardedAd()
    {

        // Verifica se o anúncio está carregado e pode ser exibido
        if (rewardedAd != null && rewardedAd.CanShowAd())
        {
            rewardedAd.Show((Reward reward) =>
            {
                // TODO: Recompense o usuário
                DiamondDataBase.diamond = DiamondDataBase.diamond + numberDiamonds; // Busca a variavel "diamond" existente dentro do script "DiamondDataBase" e adiciona um valora ela
                DiamondDataBase.diamondCurrentCondition = "Save"; // salva e atualiza o valor da variavel "diamond" existente dentro do script "DiamondDataBase"
            });
        }
    }

    // Registra os manipuladores de eventos para o anúncio recompensado
    [Obsolete]
    private void RegisterEventHandlers(RewardedAd ad)
    {
        // Evento disparado quando o anúncio é estimado para ter gerado dinheiro
        ad.OnAdPaid += (AdValue adValue) =>
        {
            Debug.Log(String.Format("Rewarded ad paid {0} {1}.",
                adValue.Value,
                adValue.CurrencyCode));
        };

        // Evento disparado quando uma impressão é registrada para um anúncio
        ad.OnAdImpressionRecorded += () =>
        {
            Debug.Log("Rewarded ad recorded an impression.");
        };

        // Evento disparado quando um clique é registrado para um anúncio
        ad.OnAdClicked += () =>
        {
            Debug.Log("Rewarded ad was clicked.");
        };

        // Evento disparado quando o conteúdo em tela cheia do anúncio é aberto
        ad.OnAdFullScreenContentOpened += () =>
        {
            Debug.Log("Rewarded ad full screen content opened.");
        };

        // Evento disparado quando o conteúdo em tela cheia do anúncio é fechado
        ad.OnAdFullScreenContentClosed += () =>
        {
            Debug.Log("Rewarded ad full screen content closed.");
            LoadRewardedAd();
        };

        // Evento disparado quando falha ao abrir o conteúdo em tela cheia do anúncio
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
        // Este método é chamado a cada quadro (frame) do jogo, mas está vazio neste exemplo
    }
}

