using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    public float sensitivity = 1;

    float mouseX;
    float mouseY;
    Vector2 mousePosition;
    Rigidbody2D soulRigidbody;
    void Start()
    {
        soulRigidbody = GetComponent<Rigidbody2D>();

        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }
    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        MoveSoul();
    }
    void MoveSoul()
    {
        soulRigidbody.MovePosition(new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, transform.position.z) + transform.position);
    }
    void OldMoveSoul()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        soulRigidbody.MovePosition(new Vector3(mouseX, mouseY) * sensitivity * Time.deltaTime + transform.position);
    }
}
