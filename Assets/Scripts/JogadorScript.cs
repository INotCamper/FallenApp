using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;

public class JogadorScript : MonoBehaviour
{
    public Item item;
    public Image iconeJogador;
    public Text nomeJogador;
    public List<Item> items;
    [Min(0)]
    public static int teste;
    public Button botaoCompra;

    public void Compra()
    {
        if (!item.comprado)
        {
            Debug.Log("Comprando jogador.");
            //Tirar o quanto custou
            item.estaNoTimeAtual = false;
            Inventario.instance.AdicionarInventario(item);
            item.comprado = true;
            botaoCompra.interactable = false;
        }
        else
        {
            Debug.Log("Jogador já foi comprado");
        }
    }
    private void Awake()
    {
        iconeJogador = transform.Find("JogadorBackground").Find("ImagemJogador").GetComponent<Image>();
        nomeJogador = GetComponentInChildren<Transform>().GetComponentInChildren<Text>();
        StartCoroutine(testeCorrotine());
    }
    IEnumerator testeCorrotine()
    {
        yield return new WaitForSeconds(.25f);
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].name.ToLower() == this.name.ToLower())
            {
                item = items[i];
                iconeJogador.sprite = item.icone;
                item.comprado = false;          //MUDAR ISSO PRA BUILDAR
            }
        }
    }
}
