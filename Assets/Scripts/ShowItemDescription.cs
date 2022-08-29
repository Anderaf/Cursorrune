using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShowItemDescription : MonoBehaviour
{
    public string description = "This is a long default description for an item or act/magic";
    [SerializeField] TMP_Text text;
    public void ChangeDescription()
    {
        text.text = description;
    }
    public void RemoveDescription()
    {
        text.text = "";
    }
}
