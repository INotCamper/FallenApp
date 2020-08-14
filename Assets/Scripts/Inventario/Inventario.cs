using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventario : MonoBehaviour
{
    #region instanciaInventario

    public static Inventario instance;
    private void Awake()
    {
        if(instance != null)
        {
            Debug.Log("Mais de uma instancia, algo está muito errado");
        }
        instance = this;
    }

    #endregion

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;

    public int espaco = 5;

    public List<Item> items = new List<Item>();

    public bool AdicionarInventario (Item item)
    {
        if(items.Count >= espaco)
        {
            Debug.Log("Sem espaco no inventario");
            return false;
        }
        items.Add(item);
        if(onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
        return true;
    }
    public void RemoverInventario(Item item)
    {
        items.Remove(item);
        if (onItemChangedCallback != null)
        {
            onItemChangedCallback.Invoke();
        }
    }
}
