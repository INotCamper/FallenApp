using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoginCanvasScript : MonoBehaviour
{
    public void BotaoNextScene()
    {
        SceneManager.LoadScene(1);
    }
}
