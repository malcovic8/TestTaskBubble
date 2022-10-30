using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BubbleController : MonoBehaviour
{
    public event Action<BubbleView> OnAddNewBubble = delegate {  };
    
    public event Func<Color[]> OnRemainigColor;
    
    [SerializeField] 
    private GameObject boxBubble = default;
    
    [SerializeField] 
    private Transform parent = default;
    
    [SerializeField] 
    private Transform pointBubble = default;

    [SerializeField]
    private BubbleView bubbleView = default;
    
    [SerializeField] 
    private float speed = default;
    
    private Coroutine coroutineMove = default;
    private BubbleView shootBubble;

    private void Start()
    {
        CreateBubble();
    }

    public void ShootBubble(Vector3 clickedPosition)
    {
        if (coroutineMove == null)
        {
            coroutineMove = StartCoroutine(MoveBubble(clickedPosition, shootBubble));
            StartCoroutine(WaitCreateBubble(1f));
        }
    }

    private void CreateBubble()
    {
        shootBubble = Instantiate<BubbleView>(bubbleView, pointBubble.position, Quaternion.identity, parent);
        Color[] remainingColor = OnRemainigColor();
        Color setColor = remainingColor[Random.Range(0, remainingColor.Length - 1)];
        shootBubble.Init(setColor);
        shootBubble.OnContactBubble += BallCrossing;
        shootBubble.OnContactWall += WallCrossing;
    }

    private void StopShootBall(BubbleView bubbleView)
    {
        StopCoroutine(coroutineMove);
        bubbleView.OnContactBubble -= BallCrossing;
        bubbleView.OnContactWall -= WallCrossing;
        OnAddNewBubble(bubbleView);
        coroutineMove = null;
    }

    private void BallCrossing(BubbleView bubble)
    {
        StopShootBall(bubble);
    }
    
    
    private void WallCrossing(BubbleView bubbleView)
    {
        
    }

    private void CollisionBubbleOther(BubbleView bubble)
    {
        
    }

    private IEnumerator WaitCreateBubble(float time)
    {
        yield return new WaitForSeconds(time);
        CreateBubble();
    }

    private IEnumerator MoveBubble(Vector2 toPosition, BubbleView bubbleView)
    {
        while (isActiveAndEnabled)
        {
            bubbleView.transform.position = Vector2.MoveTowards(bubbleView.transform.position, toPosition, speed * Time.deltaTime);
            yield return null;
        }
    }
}
