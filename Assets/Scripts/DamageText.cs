using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject canvasWithDamageText;
    [SerializeField] float damageTextOffset = 0.7f;
    List<GameObject> activeEnemyDamageCanvases = new List<GameObject>();
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /*public void ShowDamage(int _damage, Color color)
    {
        Vector3 offset = Vector2.up * activeEnemyDamageCanvases.Count * damageTextOffset;
        var canvasObject = Instantiate(canvasWithDamageText, transform.position + offset, Quaternion.identity);
        activeEnemyDamageCanvases.Add(canvasObject.gameObject);
        var textObject = canvasObject.transform.GetChild(0).GetComponent<TMP_Text>();
        textObject.text = _damage.ToString();
        textObject.faceColor = color;
        StartCoroutine(DestroyAndRemove(canvasObject.gameObject, 4));
    }*/
    public void ShowDamage(string _damage, Color color)
    {
        Vector3 offset = Vector2.up * activeEnemyDamageCanvases.Count * damageTextOffset;
        var canvasObject = Instantiate(canvasWithDamageText, transform.position + offset, Quaternion.identity);
        activeEnemyDamageCanvases.Add(canvasObject.gameObject);
        var textObject = canvasObject.transform.GetChild(0).GetComponent<TMP_Text>();
        textObject.text = _damage;
        textObject.faceColor = color;
        StartCoroutine(DestroyAndRemove(canvasObject.gameObject, 4));
    }
    IEnumerator DestroyAndRemove(GameObject _object, int delay)
    {
        yield return new WaitForSeconds(delay);
        activeEnemyDamageCanvases.Remove(_object);
        Destroy(_object);
    }
}
