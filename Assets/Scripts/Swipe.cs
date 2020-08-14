using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipe : MonoBehaviour
{
    private bool seMovimentando = false;
    private float posicaoAntiga;
    [SerializeField]
    private float posicaoMax = 10, posicaoMin = -10;
    void Update()
    {
        //A comparação com o input é o quanto vai precisar se mecher pra andar
        //A comparação com a posição é o max e min q a camera pode andar
        if (Input.GetTouch(0).deltaPosition.x > 3 && !seMovimentando && transform.position.x > posicaoMin)
        {
            seMovimentando = true;
            posicaoAntiga = transform.position.x;
            StartCoroutine(MovimentacaoEsquerda());
        }
        if (Input.GetTouch(0).deltaPosition.x < -3 && !seMovimentando && transform.position.x < posicaoMax)
        {
            seMovimentando = true;
            posicaoAntiga = transform.position.x;
            StartCoroutine(MovimentacaoDireita());
        }
        if(transform.position.x > posicaoMax)
        {
            //Ter certeza q n passa da quantidade dita em cima
            transform.position = new Vector3(posicaoMax, 0, -10);
        }
        else if(transform.position.x < posicaoMin)
        {
            transform.position = new Vector3(posicaoMin, 0, -10);
        }
    }
    //O tempo é o quao rapido vai andar a camera
    IEnumerator MovimentacaoDireita()
    {
        yield return new WaitForSeconds(.1f);
        if (transform.position.x != posicaoAntiga + 5)
        {
            transform.Translate(Vector3.right);
            StartCoroutine(MovimentacaoDireita());
        }
        seMovimentando = false;
    }
    IEnumerator MovimentacaoEsquerda()
    {
        yield return new WaitForSeconds(.1f);
        if (transform.position.x != posicaoAntiga - 5)
        {
            transform.Translate(Vector3.left);
            StartCoroutine(MovimentacaoEsquerda());
        }
        seMovimentando = false;
    }
}

