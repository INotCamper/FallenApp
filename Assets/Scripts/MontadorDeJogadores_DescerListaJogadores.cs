using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.UI;

public class MontadorDeJogadores_DescerListaJogadores : MonoBehaviour
{
    public GameObject jogadorPrefab;
    public string kd, nm, pt, tm;
    [Min(0)]
    public int playerNumber;
    public DataSnapshot dados;
    public bool Desktop = false;
    public Transform posicaoCamera;
    public Vector3 posicaoInicial;
    public GameObject botaoGoToTop;
    public Vector2 velocidade;
    public float tempoSmooth;
    public bool voltarAoTopo;
    public List<GameObject> jogadoresSpawnados;
    [Min(0)]
    private int teste;
    public float posicaoPorJogador;
    void Start()
    {
        posicaoInicial = transform.localPosition;
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://fallen-app.firebaseio.com/");
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        DataCatcher();
        StartCoroutine(corrotineMontadora());
    }
    void DataCatcher()
    {
        FirebaseDatabase.DefaultInstance
            .GetReference("players").Child("blastnov19")
            .GetValueAsync().ContinueWith(task => {
                if (task.IsFaulted)
                {
                    // Handle the error...
                }
                else if (task.IsCompleted)
                {
                    DataSnapshot evento = task.Result;
                    // Do something with snapshot...
                    playerNumber = (int)evento.ChildrenCount;
                    dados = evento;
                }
            });
    }
    IEnumerator corrotineMontadora()
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < dados.ChildrenCount; i++)
        {
            kd = dados.Child(i.ToString()).Child("kd").Value.ToString();
            nm = dados.Child(i.ToString()).Child("name").Value.ToString();
            pt = dados.Child(i.ToString()).Child("points").Value.ToString();
            tm = dados.Child(i.ToString()).Child("team").Value.ToString();
            if(!ChecarJogadorExistente(nm))
            {
                GameObject jogadorAtual = Instantiate(jogadorPrefab, transform);
                jogadorAtual.transform.position = new Vector3(transform.position.x, transform.position.y - 2 * i, transform.position.z);
                jogadorAtual.name = nm;
                jogadorAtual.transform.Find("JogadorBackground").Find("NomeJogador").GetComponent<Text>().text = nm;
                jogadorAtual.transform.Find("JogadorBackground").Find("KD").Find("KDNumero").GetComponent<Text>().text = kd;
                jogadorAtual.transform.Find("JogadorBackground").Find("Team").Find("TimeTexto").GetComponent<Text>().text = tm;
                jogadorAtual.transform.Find("JogadorBackground").Find("Points").Find("PointsNumero").GetComponent<Text>().text = pt;
                jogadoresSpawnados.Add(jogadorAtual);
            }            
        }
        teste++;
    }
    void Update()
    {
        if (posicaoCamera.position.x > 11 && posicaoCamera.position.x < 13)
        {
            if (playerNumber < 4)
            {

            }
            else
            {
                if (!Desktop)
                {
                    if ((Input.GetTouch(0).deltaPosition.y > .5f || Input.GetTouch(0).deltaPosition.y < -.5f) && (transform.localPosition.y >= posicaoInicial.y && transform.localPosition.y <= posicaoPorJogador * playerNumber))
                    {
                        transform.Translate(Vector2.up * Input.GetTouch(0).deltaPosition.y * Time.deltaTime);
                    }
                    if (transform.localPosition.y > posicaoPorJogador * playerNumber)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, posicaoPorJogador * playerNumber, transform.localPosition.z);
                    }
                    else if (transform.localPosition.y < posicaoInicial.y)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, posicaoInicial.y, transform.localPosition.z);
                    }
                }
                else
                {
                    if (Input.GetButton("Vertical") && (transform.localPosition.y >= posicaoInicial.y && transform.localPosition.y <= posicaoPorJogador * playerNumber))
                    {
                        transform.Translate(Vector2.up * Input.GetAxisRaw("Vertical") * 5 * Time.deltaTime);
                    }
                    if (transform.localPosition.y > posicaoPorJogador * playerNumber)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, posicaoPorJogador * playerNumber, transform.localPosition.z);
                    }
                    else if (transform.localPosition.y < posicaoInicial.y)
                    {
                        transform.localPosition = new Vector3(transform.localPosition.x, posicaoInicial.y, transform.localPosition.z);
                    }
                }
                if(transform.localPosition.y >= posicaoInicial.y + 400)
                {
                    botaoGoToTop.SetActive(true);
                }
                else
                {
                    botaoGoToTop.SetActive(false);
                }
            }
        }
    }
    public void BotaoAtt()
    {
        DataCatcher();
        StartCoroutine(corrotineMontadora());
    }
    void FixedUpdate()
    {
        if(transform.localPosition.y > posicaoInicial.y + 5)
        {
            if (voltarAoTopo)
            {
                transform.localPosition = Vector2.SmoothDamp(transform.localPosition, posicaoInicial, ref velocidade, tempoSmooth);
            }
        }
        else
        {
            voltarAoTopo = false;
        }
    }
    public void BotaoGoToTop()
    {
        voltarAoTopo = true;
    }
    public bool ChecarJogadorExistente(string nome)
    {
        for (int i = 0; i < jogadoresSpawnados.Count; i++)
        {
            if(jogadoresSpawnados[i].name == nome)
            {
                Debug.Log("ja existe na lista e na cena");
                return true;
            }
        }
        return false;
    }
}