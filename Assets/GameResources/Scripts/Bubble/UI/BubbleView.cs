using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

/// <summary>
/// View Bubble
/// </summary>
[RequireComponent(typeof(Image), typeof(Collider2D))]
public class BubbleView : MonoBehaviour
{
    public event Action<BubbleView> OnContactWall = delegate {  };
    public event Action<BubbleView> OnContactBubble = delegate {  };
    
    private Color colorBubble = default;
    
    public Color ColorBubble
    {
        get => colorBubble;
        private set
        {
            colorBubble = value;
        }
    }
    
    private Image bubbleImage = default;
    private Collider2D collider2D = default;
    private BubbleView otherBubble = default;

    private void Awake()
    {
        bubbleImage = GetComponent<Image>();
        collider2D = GetComponent<CircleCollider2D>();
    }

    public void Init(Color color)
    {
        bubbleImage.color = color;
        colorBubble = color;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<BubbleView>(out otherBubble))
        {
            OnContactBubble(this);
        }
        else
        {
            OnContactWall(this);
        }
    }
}
