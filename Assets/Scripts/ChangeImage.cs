using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeImage : MonoBehaviour
{
    Button button;
    Sprite defaultSprite;
    [SerializeField] Sprite hoverSprite;
    void Start()
    {
        button = GetComponent<Button>();
        defaultSprite = button.image.sprite;
    }

    public void SetDefaultSprite()
    {
        button.image.sprite = defaultSprite;
    }
    public void SetHoverImage()
    {
        button.image.sprite = hoverSprite;
    }
}
