using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBridge : MonoBehaviour
{
    
    [SerializeField] 
    private BubbleMatrixController bubbleMatrixController = default;

    [SerializeField] 
    private BubbleController bubbleController = default;
    
    
    [SerializeField] 
    private ShootBubbleController shooterBubbleController = default;

    private void Awake()
    {
        shooterBubbleController.OnShootBubble += bubbleController.ShootBubble;
        bubbleController.OnAddNewBubble += bubbleMatrixController.AddBubble;
        bubbleController.OnRemainigColor += bubbleMatrixController.RemainingСolors;
    }

    private void OnDestroy()
    {
        shooterBubbleController.OnShootBubble -= bubbleController.ShootBubble;
        bubbleController.OnAddNewBubble -= bubbleMatrixController.AddBubble;
        bubbleController.OnRemainigColor -= bubbleMatrixController.RemainingСolors;
    }
}
