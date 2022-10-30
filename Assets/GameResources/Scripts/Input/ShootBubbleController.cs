using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class ShootBubbleController : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public event Action<Vector3> OnShootBubble = delegate {  };

    public void OnPointerUp(PointerEventData eventData)
    {
        OnShootBubble(eventData.position);
    }
    
    public void OnPointerDown(PointerEventData eventData)
    {
#if UNITY_ANDROID
        
#endif
    }
}
