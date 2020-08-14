using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SlotInventario : MonoBehaviour
{
    public Image iconeJogador;
    public Item item;
    public Button cliqueAddTime;
    public GameObject textoAviso;
    public void AddItem(Item newItem)
    {
        item = newItem;
        iconeJogador.sprite = item.icone;
        iconeJogador.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;
        iconeJogador.sprite = null;
        iconeJogador.enabled = false;
    }
    public void UsarItem()
    {
        if(item != null)
        {
            if (!item.estaNoTimeAtual)
            {
                TimeAtual.instance.AdicionarTimeAtual(item);
                item.estaNoTimeAtual = true;
            }
            else
            {
                StartCoroutine(AvisoTime());
            }
        }
    }
    IEnumerator AvisoTime()
    {
        textoAviso.SetActive(true);
        yield return new WaitForSeconds(2);
        textoAviso.SetActive(false);
    }
}
