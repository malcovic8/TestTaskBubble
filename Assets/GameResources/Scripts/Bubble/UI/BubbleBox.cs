using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleBox : MonoBehaviour
{
    private BubbleView bubbleView = default;

    public BubbleView Bubble
    {
        get => bubbleView;
        private set => bubbleView = value;
    }

    public void InitBubble(BubbleView bubbleView)
    {
        this.bubbleView = bubbleView;
    }

    [ContextMenu("Check Bubble")]
    public void CheckBubble()
    {
        string message = bubbleView == null ? "Null bubble" : $"{bubbleView.ColorBubble}";
        Debug.Log(message);
    } 

}
