using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using Firebase.Unity.Editor;

public class CatchAndPost : MonoBehaviour
{
    public TimeAtual timeAtual;
    int i;
    void Start()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fallen-app.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        timeAtual = GameObject.Find("GameManager").GetComponent<TimeAtual>();
    }

    public void Post()
    {
        if(FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            Debug.Log("Usuário não logado");
        }
        else
        {
            i = 0;
            foreach(Item jogador in timeAtual.itemsTA)
            {
                i++;
                FirebaseDatabase.DefaultInstance.GetReference("teams").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId).Child(i.ToString()).SetValueAsync(jogador.name);
            }
            
        }
    }

    public void Catch()
    {
        if (FirebaseAuth.DefaultInstance.CurrentUser == null)
        {
            Debug.Log("Usuário não logado");
        }
        else
        {
            FirebaseDatabase.DefaultInstance
            .GetReference("teams").Child(FirebaseAuth.DefaultInstance.CurrentUser.UserId)
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    Debug.Log("Deu merda");
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot snapshot;
                }
            });
        }      
    }
}
