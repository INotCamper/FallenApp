using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAtualUI : MonoBehaviour
{
    public Transform parenteTimeAtual;
    TimeAtual timeAtual;
    SlotTimeAtual[] slotsTA;
    void Start()
    {
        timeAtual = TimeAtual.instance;
        timeAtual.onItemChangedCallback += UpdateUI;
        slotsTA = parenteTimeAtual.GetComponentsInChildren<SlotTimeAtual>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void UpdateUI()
    {
        Debug.Log("Update UI");
        for (int i = 0; i < slotsTA.Length; i++)
        {
            if (i < timeAtual.itemsTA.Count)
            {
                slotsTA[i].AddItem(timeAtual.itemsTA[i]);
            }
            else
            {
                slotsTA[i].ClearSlot();
            }
        }
    }
}
