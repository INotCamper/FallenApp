using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipev2 : MonoBehaviour
{
    public bool seMovimentando = false;
    public Transform[] posicoesCamera = new Transform[5];
    [Range(0, 3)]
    public static int posicaoAtual;
    public Vector3 velocidade = Vector3.zero;
    public float tempoSmooth;
    public bool Desktop = false;
    public static float posicaoAntigaMouse;
    void Update()
    {
        if (!Desktop)
        {
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (!seMovimentando && Input.GetTouch(0).deltaPosition.x > 75 && posicaoAtual > 0)
                {
                    posicaoAtual--;
                }
                if (!seMovimentando && Input.GetTouch(0).deltaPosition.x < -75 && posicaoAtual < 3)
                {
                    posicaoAtual++;
                }
            }
        }
        else
        {
            if (Input.GetButtonDown("Horizontal"))
            {
                if (!seMovimentando && Input.GetAxis("Horizontal") < 0 && posicaoAtual > 0)
                {
                    posicaoAtual--;
                }
                if (!seMovimentando && Input.GetAxis("Horizontal") > 0 && posicaoAtual < 3)
                {
                    posicaoAtual++;
                }
            }
            if (Input.GetMouseButtonUp(0))
            {
                if(Input.GetAxis("Mouse X") > .1f && posicaoAtual > 0)
                {
                    posicaoAtual--;
                }
                else if(Input.GetAxis("Mouse X") < -.1f && posicaoAtual < 3)
                {
                    posicaoAtual++;
                }
            }
        }        
    }
    private void FixedUpdate()
    {
        AndarCamera();
    }
    void AndarCamera()
    {
        Vector3 targetPosition = posicoesCamera[posicaoAtual].TransformPoint(Vector3.zero);
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocidade, tempoSmooth);
        if(transform.position.x < posicoesCamera[posicaoAtual].position.x +.01f && transform.position.x > posicoesCamera[posicaoAtual].position.x - .01f)
        {
            seMovimentando = false;
        }
    }
}
