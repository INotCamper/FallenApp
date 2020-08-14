using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Firebase.Auth;

public class CanvasProfile : MonoBehaviour
{
    public Text textoBotaoPontos;
    public Text olaUsuario;
    Firebase.Auth.FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    void Awake()
    {
        olaUsuario.text = "Ola " + FirebaseAuth.DefaultInstance.CurrentUser.DisplayName;
    }

    // Update is called once per frame
    public void SignOut()
    {
        FirebaseAuth.DefaultInstance.SignOut();
        SceneManager.LoadScene("Login");
    }
    public void BackButton()
    {
        SceneManager.UnloadSceneAsync(2);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
    IEnumerator EmBreve()
    {
        textoBotaoPontos.text = "!!!Em Breve!!!";
        yield return new WaitForSeconds(1);
        textoBotaoPontos.text = "Resgatar Pontos";
    }
    public void BotaoPontos()
    {
        StartCoroutine(EmBreve());
    }
}
