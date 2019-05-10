using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AddLinkToListCommand : Command
{
    ScrollRect scrollRect;
    LinkHandler linkHandler;
    LinkHandler instantiatedLinkHandler;
    private int Index;

    public AddLinkToListCommand(ScrollRect scrollRect, LinkHandler linkHandler,int index)
    {
        this.linkHandler = linkHandler;
        this.scrollRect = scrollRect;
        Index = index;
    }
    public override void Execute()
    {
        if (instantiatedLinkHandler == null)
        {
            GameObject instantiatedObject = Object.Instantiate(linkHandler.gameObject, scrollRect.content.transform) as GameObject;
            instantiatedObject.GetComponent<RectTransform>().sizeDelta = linkHandler.GetComponent<RectTransform>().sizeDelta;
            instantiatedLinkHandler = instantiatedObject.GetComponent<LinkHandler>();
            instantiatedObject.transform.SetSiblingIndex(Index);
            instantiatedLinkHandler.AddedToList();
            instantiatedLinkHandler.positionInChain = Index;
        }
        else
        {
            instantiatedLinkHandler.gameObject.SetActive(true);
        }
        instantiatedLinkHandler.transform.parent = scrollRect.content.transform;
    }
    public override void Undo()
    {
        base.Undo();
        instantiatedLinkHandler.gameObject.SetActive(false);
        instantiatedLinkHandler.transform.parent = null;
    }
    public override void Delete()
    {
        base.Delete();
        Object.Destroy(instantiatedLinkHandler.gameObject);
    }

}
