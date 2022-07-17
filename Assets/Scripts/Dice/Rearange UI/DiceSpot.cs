using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DiceSpot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [SerializeField]
    private int position;

    public int Position => position;

    [SerializeField]
    private DieVisual die;

    public DieVisual Die => die;

    public Action<DiceSpot> MouseOver, MouseLeave, OnClick;

    public void SetDie(DieVisual die){
        if(die != null){
            die.transform.SetParent(transform, false);
            die.transform.localPosition = Vector3.zero;
        }
        this.die = die;
    }

    // private void OnMouseEnter() {
        
        
    // } 

    //  private void OnMouseExit() {
    //      MouseLeave?.Invoke(this);
    //  }

    public void OnPointerEnter(PointerEventData eventData)
    {
        MouseOver?.Invoke(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        MouseLeave?.Invoke(this);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke(this);
    }
}
