using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] List<ItemObject> items = new List<ItemObject>();
    
    public ItemObject GetItem(int id)
    {
        if (id < items.Count)
        {
            return items[id];
        }
        else
        {
            return null;
        }      
    }
    public List<ItemObject> GetItems()
    {
        return items;
    }
    public void DeleteItem(int id)
    {
        if (id < items.Count)
        {
            items.RemoveAt(id);
        }
        else
        {
            Debug.LogWarning("Tried to delete item that doesn't exist(wrong index)");
        }
        
    }
}
