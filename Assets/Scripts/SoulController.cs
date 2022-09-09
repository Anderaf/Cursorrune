using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoulController : MonoBehaviour
{
    [SerializeField] AudioClip hurtSound;

    public float sensitivity = 1;
    float mouseX;
    float mouseY;

    bool inControl = true;
    bool hurtAnimation;

    Vector3 battleStartPosition;
    Vector3 krisSoulSpot;
    Vector2 mousePosition;

    Color initialColor;

    Rigidbody2D soulRigidbody;
    CursorFollow dotCursor;   
    SpriteRenderer spriteRenderer;
    BattleManager battleManager;
 
    [Header("Hurt Animation")]
    public float hurtAnimationTime = 0.3f;
    float hurtAnimationTimer;

    public float invincibilityTime = 2f;
 
    void Start()
    {
        battleManager = FindObjectOfType<BattleManager>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        initialColor = spriteRenderer.color;
        hurtAnimationTimer = hurtAnimationTime;
        soulRigidbody = GetComponent<Rigidbody2D>();

        //Cursor.lockState = CursorLockMode.Locked;
        //Cursor.visible = false;
        battleStartPosition = transform.position;
        dotCursor = FindObjectOfType<CursorFollow>();
        krisSoulSpot = FindObjectOfType<Kris>().transform.position + new Vector3(0.6f,0.6f,0.1f);
        DisableVisibility();
        StopControl();
        transform.position = krisSoulSpot;
    }

    private void FixedUpdate()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if (inControl)
        {
            MoveSoul();
        }


        if (hurtAnimation)
        {
            hurtAnimationTimer -= Time.deltaTime;
            if (hurtAnimationTimer <= 0)
            {
                HurtAnimationColorSwitch();
                hurtAnimationTimer = hurtAnimationTime;
            }
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
        dotCursor.DisableVisibility();
    }
    void StartControl()
    {
        GetComponent<Collider2D>().enabled = true;
        inControl = true;
        dotCursor.EnableVisibility();
    }
    public void StartBattleMode()
    {
        StartCoroutine(ToBattleStartPositionCoroutine());
        StopControl();
        Invoke("StartControl", 1);
        Cursor.visible = false;
        EnableVisibility();
    }
    public void EndBattleMode()
    {
        StartCoroutine(ToPositionCoroutine());
        StopControl();
        Cursor.visible = true;
        Invoke("DisableVisibility", 1);
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
    IEnumerator ToPositionCoroutine()
    {
        float _distance = Vector3.Distance(transform.position, krisSoulSpot);
        while (transform.position != krisSoulSpot)
        {
            transform.position = Vector3.MoveTowards(transform.position, krisSoulSpot, 0.01f * _distance);
            yield return new WaitForSeconds(0.005f);
        }
        yield return new WaitForSeconds(0);
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
    void HurtAnimationColorSwitch()
    {
        float colorDifference = 0.2f;
        if (spriteRenderer.color == initialColor)
        {
            spriteRenderer.color = new Color(spriteRenderer.color.r - colorDifference, spriteRenderer.color.g - colorDifference, spriteRenderer.color.b - colorDifference, spriteRenderer.color.a);
        }
        else
        {
            spriteRenderer.color = initialColor;
        }
    }
    public void TakeDamage(int _damage)
    {
        if (!hurtAnimation)
        {
            battleManager.soundPlayer.PlayOneShot(hurtSound);
            HurtAnimationColorSwitch();
            battleManager.TakeDamage(_damage);
            StartCoroutine(HurtSwitchCoroutine());
        }        
    }
    IEnumerator HurtSwitchCoroutine()
    {
        hurtAnimation = true;
        yield return new WaitForSeconds(invincibilityTime);
        hurtAnimation = false;
        hurtAnimationTimer = hurtAnimationTime;
        spriteRenderer.color = initialColor;
    }
}
