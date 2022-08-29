using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeColorOnHover : MonoBehaviour
{
    [SerializeField] Color hoverColor = Color.yellow;
    Color defaultColor;
    TMP_Text text;
    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        defaultColor = text.color;
    }
    void Start()
    {
        /*text = GetComponent<TMP_Text>();
        defaultColor = text.color;*/
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetDefaultColor()
    {
        text.color = defaultColor;
    }
    public void SetHoverColor()
    {
        text.color = hoverColor;
    }
}
