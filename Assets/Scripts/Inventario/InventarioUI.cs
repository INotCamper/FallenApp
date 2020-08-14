using UnityEngine;

public class InventarioUI : MonoBehaviour
{
    public Transform parenteItens;
    Inventario inventario;
    SlotInventario[] slots;
    void Start()
    {
        inventario = Inventario.instance;
        inventario.onItemChangedCallback += UpdateUI;
        slots = parenteItens.GetComponentsInChildren<SlotInventario>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void UpdateUI()
    {
        Debug.Log("Update UI");
        for(int i = 0; i < slots.Length; i++)
        {
            if(i < inventario.items.Count)
            {
                slots[i].AddItem(inventario.items[i]);
            }
            else
            {
                slots[i].ClearSlot();
            }
        }
    }
}
