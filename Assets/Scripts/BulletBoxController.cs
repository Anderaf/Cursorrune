using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BulletBoxController : MonoBehaviour
{
    Animator animator;
    SpriteShapeController spriteShapeController;
    Spline spline;
    Vector2 topLeft;
    Vector2 topRight;
    Vector2 bottomRight;
    Vector2 bottomLeft;
    [SerializeField] Vector2 topLeftTarget;
    [SerializeField] Vector2 topRightTarget;
    [SerializeField] Vector2 bottomRightTarget;
    [SerializeField] Vector2 bottomLeftTarget;
    EdgeCollider2D edgeCollider;
    SoulController soul;

    public float changeSpeed = 0.1f;
    void Start()
    {
        soul = FindObjectOfType<SoulController>();
        animator = GetComponent<Animator>();
        edgeCollider = GetComponent<EdgeCollider2D>();
        spriteShapeController = GetComponent<SpriteShapeController>();
        spline = spriteShapeController.spline;
        /*bottomLeft = edgeCollider.points[0];
        topLeft = edgeCollider.points[1];
        topRight = edgeCollider.points[2];
        bottomRight = edgeCollider.points[3];*/

        bottomLeft = spline.GetPosition(0);
        topLeft = spline.GetPosition(1);
        topRight = spline.GetPosition(2);
        bottomRight = spline.GetPosition(3);

        topLeftTarget = topLeft;
        topRightTarget = topRight;
        bottomRightTarget = bottomRight;
        bottomLeftTarget = bottomLeft;

        Invoke("StartSpawnAnimation", 1);
        Invoke("StartDespawnAnimation", 10);
    }

    void Update()
    {
        UpdateSpline();
        UpdateEdgeCollider();
        UpdatePositions();
    }
    void UpdateSpline()
    {
        spline.SetPosition(0, bottomLeft);
        spline.SetPosition(1, topLeft);
        spline.SetPosition(2, topRight);
        spline.SetPosition(3, bottomRight);
    }
    void UpdateEdgeCollider()
    {
        List<Vector2> colPoints = new List<Vector2>() { bottomLeft + new Vector2(0.25f,0.25f), topLeft + new Vector2(0.25f, -0.25f), topRight + new Vector2(-0.25f, -0.25f), bottomRight + new Vector2(-0.25f, 0.25f), bottomLeft + new Vector2(0.25f, 0.25f) };
        edgeCollider.SetPoints(colPoints);
    }
    void UpdatePositions()
    {
        bottomLeft = Vector2.MoveTowards(bottomLeft, bottomLeftTarget, changeSpeed);
        topLeft = Vector2.MoveTowards(topLeft, topLeftTarget, changeSpeed);
        topRight = Vector2.MoveTowards(topRight, topRightTarget, changeSpeed);
        bottomRight = Vector2.MoveTowards(bottomRight, bottomRightTarget, changeSpeed);
    }
    void StartSpawnAnimation()
    {
        animator.SetTrigger("Spawn");
        soul.StartBattleMode();
    }
    void StartDespawnAnimation()
    {
        animator.SetTrigger("Despawn");
    }
    public void MoveRightEdge(Vector2 direction)
    {
        topRightTarget += direction;
        bottomRightTarget += direction;
    }
    public void MoveTopEdge(Vector2 direction)
    {
        topLeftTarget += direction;
        topRightTarget += direction;
    }
    public void MoveLeftEdge(Vector2 direction)
    {
        bottomLeftTarget += direction;
        topLeftTarget += direction;
    }
    public void MoveBottomEdge(Vector2 direction)
    {
        bottomLeftTarget += direction;
        bottomRightTarget += direction;
    }
}
