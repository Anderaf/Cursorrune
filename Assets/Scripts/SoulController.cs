using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoulController : MonoBehaviour
{
    public float sensitivity = 1;

    float mouseX;
    float mouseY;
    bool inControl = true;
    Vector3 battleStartPosition;
    Vector2 mousePosition;
    Rigidbody2D soulRigidbody;
    void Start()
    {
        soulRigidbody = GetComponent<Rigidbody2D>();

        //Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        battleStartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        

        
    }
    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (inControl)
        {
            MoveSoul();
        }    
    }
    void MoveSoul()
    {
        soulRigidbody.MovePosition(new Vector3(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y, transform.position.z) + transform.position);
    }
    void StopControl()
    {
        GetComponent<Collider2D>().enabled = false;
        inControl = false;
    }
    void StartControl()
    {
        GetComponent<Collider2D>().enabled = true;
        inControl = true;
    }
    public void StartBattleMode()
    {
        StartCoroutine(ToBattleStartPositionCoroutine());
        StopControl();
        Invoke("StartControl", 1);
    }
    
    void OldMoveSoul()
    {
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        soulRigidbody.MovePosition(new Vector3(mouseX, mouseY) * sensitivity * Time.deltaTime + transform.position);
    }
    IEnumerator ToBattleStartPositionCoroutine()
    {
        float _distance = Vector3.Distance(transform.position, battleStartPosition);
        while(transform.position != battleStartPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, battleStartPosition, 0.01f * _distance);
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(0);
    }
}
