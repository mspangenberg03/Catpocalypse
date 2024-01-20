using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.EventSystems;


public class TowerSelectButtonMouseOver : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public TowerInfo.TowerTypes TowerType;


    public event EventHandler<MouseOverEventArgs> OnMouseEnter;
    public event EventHandler<MouseOverEventArgs> OnMouseExit;
    public class MouseOverEventArgs : EventArgs
    {
        public TowerInfo.TowerTypes TowerType;
    }




    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData) 
    {        
        OnMouseEnter?.Invoke(this, new MouseOverEventArgs()
        {
            TowerType = TowerType,
        });
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData) 
    {
        OnMouseExit?.Invoke(this, new MouseOverEventArgs()
        {
            TowerType = TowerType,
        });
        
    }

    public float GetWidth()
    {
        return GetComponent<RectTransform>().rect.width;
    }
}
