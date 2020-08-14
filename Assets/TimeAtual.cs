using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAtual : MonoBehaviour
{
    #region instanciaTime

    public static TimeAtual instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Mais de uma instancia, algo está muito errado");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int espaco = 5;

    public List<Item> itemsTA = new List<Item>();

    public bool AdicionarTimeAtual(Item item)
    {
        if (itemsTA.Count >= espaco)
        {
            Debug.Log("Sem espaco no inventario");
            return false;
        }
        itemsTA.Add(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }
    public void RemoverTimeAtual(Item item)
    {
        itemsTA.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
