using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour
{
    Transform currentTransform;
    Vector3 targetPosition;
    Vector3 initialPosition;
    private void Awake()
    {
        currentTransform = transform;
        initialPosition = currentTransform.position;
        targetPosition = initialPosition + Vector3.up * 45;
    }
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    public void Open()
    {
        StartCoroutine(ToPositionCoroutine(targetPosition));
    }
    public void Close()
    {
        StartCoroutine(ToPositionCoroutine(initialPosition));
    }
    IEnumerator ToPositionCoroutine(Vector3 target)
    {
        float _distance = Vector3.Distance(currentTransform.position, target);
        while (currentTransform.position != target)
        {
            currentTransform.position = Vector3.MoveTowards(currentTransform.position, target, 0.04f * _distance);
            yield return new WaitForSeconds(0.005f);
        }
        currentTransform.position = target;
        yield return new WaitForSeconds(0);
    }
    
}
