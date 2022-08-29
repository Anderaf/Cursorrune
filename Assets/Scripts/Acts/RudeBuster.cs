using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RudeBuster : ActScript
{
    [SerializeField] GameObject rudeBusterPrefab;
    [SerializeField] GameObject explosionFXPrefab;
    [SerializeField] GameObject curve;
    [SerializeField] int damage = 300;

    [Header("Buster movement")]
    [SerializeField] float speedModifier = 1;
    [SerializeField] float accelerationModifier = 0.02f;
    Transform[] routes = new Transform[1];

    Enemy currentEnemy;
    Transform currentEnemyTransform;
    GameObject rudeBuster;
  
    int routeToGo = 0;
    float tParam = 0;
    Vector2 objectPosition;
    bool coroutineAllowed = true;


    public override void StartAct()
    {
        //routeToGo = 0;
        //tParam = 0f;
        //coroutineAllowed = true;

        currentEnemy = enemies[targetID];
        currentEnemyTransform = currentEnemy.transform;
        rudeBuster = Instantiate(rudeBusterPrefab, actingCharacter.transform.GetChild(0).position, Quaternion.identity);

        Transform curveTransform = curve.transform;
        curveTransform.GetChild(0).position = actingCharacter.transform.GetChild(0).position;
        curveTransform.GetChild(1).position = actingCharacter.transform.GetChild(0).position + new Vector3(2, -1, 0);
        curveTransform.GetChild(2).position = enemies[targetID].transform.position + new Vector3(-2, -2, 0);
        curveTransform.GetChild(3).position = enemies[targetID].transform.position;

        routes[0] = curveTransform;
        actingCharacter.GetComponent<Animator>().Play("ShootBuster");
        actingCharacter.ResetBools();
    }


    void Update()
    {
        if (coroutineAllowed)
        {
            StartCoroutine(GoByTheRoute(routeToGo));
        }
        /*if (rudeBuster.transform.position == enemies[targetID].transform.position)
        {
            Destroy(Instantiate(explosionFXPrefab, enemies[targetID].transform.position, Quaternion.identity), 2);
            Destroy(rudeBuster);
        }*/
    }

    private IEnumerator GoByTheRoute(int routeNum)
    {
        coroutineAllowed = false;

        Vector2 p0 = routes[routeNum].GetChild(0).position;
        Vector2 p1 = routes[routeNum].GetChild(1).position;
        Vector2 p2 = routes[routeNum].GetChild(2).position;
        Vector2 p3 = routes[routeNum].GetChild(3).position;

        while (tParam < 1)
        {
            tParam += Time.deltaTime * speedModifier;
            speedModifier += accelerationModifier;

            var previousPosition = objectPosition;
            objectPosition = Mathf.Pow(1 - tParam, 3) * p0 + 3 * Mathf.Pow(1 - tParam, 2) * tParam * p1 + 3 * (1 - tParam) * Mathf.Pow(tParam, 2) * p2 + Mathf.Pow(tParam, 3) * p3;
            var movementDirection = objectPosition - previousPosition;
            rudeBuster.transform.right = movementDirection;

            rudeBuster.transform.position = objectPosition;
            yield return new WaitForEndOfFrame();
        }

        tParam = 0f;

        routeToGo += 1;

        if (routeToGo > routes.Length - 1)
        {
            routeToGo = 0;
        }

        Destroy(Instantiate(explosionFXPrefab, enemies[targetID].transform.position - Vector3.forward * 0.001f, Quaternion.Euler(0,0,45)), 2);
        Destroy(rudeBuster);
        enemies[targetID].TakeDamage(damage,actingCharacter.GetColor(),false);

    }
}
