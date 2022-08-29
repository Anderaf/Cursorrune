using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorFollow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePosition.x, mousePosition.y, transform.position.z);
    }
    public void EnableVisibility()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 255;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
    public void DisableVisibility()
    {
        Color color = gameObject.GetComponent<SpriteRenderer>().color;
        color.a = 0;
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }
}
