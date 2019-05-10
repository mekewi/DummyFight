using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragHandler : MonoBehaviour,IDragHandler,IEndDragHandler
{
    public RectTransform myRectTransform;
    public Vector3 localPosition;
    public DummyHandler dummyHandler;
    private void Start()
    {
        myRectTransform = GetComponent<RectTransform>();
        localPosition = myRectTransform.localPosition;
        dummyHandler = GetComponent<DummyHandler>();
    }
    void Update()
    {
    }
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
        GameManager.Instance.DoScrollOnDrag();
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        GameManager.Instance.linkDropEnded(GetComponent<LinkHandler>());
        gameObject.SetActive(false);//= false;
        gameObject.SetActive(true);//= false;
    }
}
