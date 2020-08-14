using UnityEngine;
using UnityEngine.UI;

public class SlotTimeAtual : MonoBehaviour
{
    public Image imagemJogador;
    public Item item;
    public void AddItem(Item newItem)
    {
        item = newItem;
        imagemJogador.sprite = item.icone;
        imagemJogador.enabled = true;
    }
    public void ClearSlot()
    {
        item = null;
        imagemJogador.sprite = null;
        imagemJogador.enabled = false;
    }
    public void UsarItem()
    {
        if (item != null)
        {
            item.estaNoTimeAtual = false;
            TimeAtual.instance.RemoverTimeAtual(item);
        }
    }
}
