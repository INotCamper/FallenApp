using UnityEngine;

[CreateAssetMenu(fileName = "Novo Item", menuName = "Inventario/Item")]
public class Item : ScriptableObject
{
    new public string name = "new item";    //Nome do item(jogador)
    public Sprite icone = null;             //Icone do item(jogador)
    public bool comprado = false;       //Se já foi comprado
    public bool estaNoTimeAtual;        //Se esta no time atual

    public virtual void Usar()
    {
        //fazer algo
        Debug.Log("Usando " + name);
    }
}
