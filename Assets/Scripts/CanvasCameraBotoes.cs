using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasCameraBotoes : MonoBehaviour
{
    public void BotaoHome()
    {
        Swipev2.posicaoAtual = 0;
    }
    public void BotaoLeader()
    {
        Swipev2.posicaoAtual = 1;
    }
    public void BotaoPlayer()
    {
        Swipev2.posicaoAtual = 2;
    }
    public void BotaoMarket()
    {
        Swipev2.posicaoAtual = 3;
    }
    public void BotaoProfile()
    {
        SceneManager.LoadScene(2);
    }
}
