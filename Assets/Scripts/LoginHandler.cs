using UnityEngine;
using UnityEngine.UI;
using Firebase.Auth;
using UnityEngine.SceneManagement;

public class LoginHandler : MonoBehaviour
{
    FirebaseAuth auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
    FirebaseUser user;
    public InputField Iemail, Ipassword, IconfirmPassword, Iuser, IloginEmail, IloginPassword;
    public string email, password, confirmPassword;
    public bool Equal;
    public GameObject mensagemErro, mensagemSucesso;
    public Transform targetMensagem;


    public void LoginDataCatcher()
    {
        email = IloginEmail.text;
        password = IloginPassword.text;

        DefaultLogin(email, password);
    }

    public void SignUpDataCatcher()
    {
        email = Iemail.text;
        if (Ipassword.text == IconfirmPassword.text)
        {
            password = Ipassword.text;
            Equal = true;
        }
        else
        {
            Debug.LogFormat("Senhas não coincidem");
            Equal = false;
        }
        if (Equal)
        {
            NewUser(email, password);
        }
    }

    void DefaultLogin(string email, string password)
    {
        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    return;
            }

            user = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                user.DisplayName, user.UserId);
            SceneManager.UnloadSceneAsync(0);
            SceneManager.LoadScene(1, LoadSceneMode.Additive);

        });
    }

    void NewUser(string email, string password)
    {
        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                GameObject mensagemErr = Instantiate(mensagemErro);
                mensagemErr.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
                mensagemErr.transform.position = new Vector3(targetMensagem.position.x, targetMensagem.position.y * 2, targetMensagem.position.z);
                Debug.LogError("CreateUserWithEmailAndPasswordAsync error: " + task.Exception);
                if (task.Exception.InnerExceptions.Count > 0)
                    return;
            }

            FirebaseUser newUser = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                newUser.DisplayName, newUser.UserId);
            GameObject mensagemSuc = Instantiate(mensagemSucesso);
            mensagemSuc.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform.transform, false);
            mensagemSuc.transform.position = new Vector3(targetMensagem.position.x, targetMensagem.position.y * 2, targetMensagem.position.z);
        });
    }
    public void SecretButton()
    {
        SceneManager.UnloadSceneAsync(0);
        SceneManager.LoadScene(1, LoadSceneMode.Additive);
    }
}
