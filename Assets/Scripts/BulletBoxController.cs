using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class BulletBoxController : MonoBehaviour
{
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

    public float changeSpeed = 0.1f;
    void Start()
    {
        edgeCollider = GetComponent<EdgeCollider2D>();
        spriteShapeController = GetComponent<SpriteShapeController>();
        spline = spriteShapeController.spline;
        bottomLeft = edgeCollider.points[0];
        topLeft = edgeCollider.points[1];
        topRight = edgeCollider.points[2];
        bottomRight = edgeCollider.points[3];
        topLeftTarget = topLeft;
        topRightTarget = topRight;
        bottomRightTarget = bottomRight;
        bottomLeftTarget = bottomLeft;
    }

    // Update is called once per frame
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
        /*Mathf.Lerp(bottomLeft.x, bottomLeftTarget.x, changeSpeed);
        Mathf.Lerp(bottomLeft.y, bottomLeftTarget.y, changeSpeed);
        Mathf.Lerp(topLeft.x, topLeftTarget.x, changeSpeed);
        Mathf.Lerp(topLeft.y, topLeftTarget.y, changeSpeed);
        Mathf.Lerp(topRight.x, topRightTarget.x, changeSpeed);
        Mathf.Lerp(topRight.y, topRightTarget.y, changeSpeed);
        Mathf.Lerp(bottomRight.x, bottomRightTarget.x, changeSpeed);
        Mathf.Lerp(bottomRight.y, bottomRightTarget.y, changeSpeed);*/
        bottomLeft = Vector2.MoveTowards(bottomLeft, bottomLeftTarget, changeSpeed);
        topLeft = Vector2.MoveTowards(topLeft, topLeftTarget, changeSpeed);
        topRight = Vector2.MoveTowards(topRight, topRightTarget, changeSpeed);
        bottomRight = Vector2.MoveTowards(bottomRight, bottomRightTarget, changeSpeed);
    }
    public void MoveRightEdge(float distance)
    {

    }
}
