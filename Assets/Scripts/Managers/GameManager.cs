using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    public RectTransform dropPanel;
    public RectTransform content;
    public LinkHandler currentActiveLink;
    public List<LinkDetails> linksData = new List<LinkDetails>();
    public GameObject LinkPrefab;
    public Canvas canvas;
    public void LinksLoaded()
    {
        foreach (var item in LinksManager.Instance.linksData)
        {
            InstantiateLinks(item);
        }
    }
    private void Start()
    {
        LinksManager.Instance.LoadLinks();
    }
    public void Fight() 
    {
        HandlLinksData();
        SceneManager.LoadScene("Fight");
    }
    public void HandlLinksData() 
    {
        Transform contentOfLinks = dropPanel.GetComponent<ScrollRect>().content;
        foreach (Transform link in contentOfLinks)
        {
            LinksManager.Instance.AddLink(link.GetComponent<LinkHandler>().linkDetails);
        }

    }
    public void linkDropEnded(LinkHandler linkHandler)
    {
        if (RectTransformUtility.RectangleContainsScreenPoint(dropPanel, Input.mousePosition))
        {
            int Index = CalculateIndexInChain(linkHandler.GetComponent<RectTransform>().sizeDelta.x);
            if (linkHandler.isInChainList && Index != linkHandler.positionInChain)
            {
                ChangePositionInsideListCommand changePositionInsideListCommand = new ChangePositionInsideListCommand(Index,linkHandler);
                CommandsManager.Instance.ExcuteCommand(changePositionInsideListCommand);

            }
            else
            {
                AddLinkToListCommand addLinkToListCommand = new AddLinkToListCommand(dropPanel.GetComponent<ScrollRect>(), linkHandler, Index);
                CommandsManager.Instance.ExcuteCommand(addLinkToListCommand);
            }
        }
    }
    public int CalculateIndexInChain(float WidthSize) 
    {
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(content, Input.mousePosition, canvas.worldCamera, out localpoint);
        return (int)Mathf.Ceil (localpoint.x / (WidthSize+10))-1;
    }
    public void InstantiateLinks(LinkDetails linkDetails)
    {
        GameObject instantiatedObject = Instantiate(LinkPrefab, dropPanel.GetComponent<ScrollRect>().content.transform);
        LinkHandler instantiatedLinkHandler = instantiatedObject.GetComponent<LinkHandler>();
        instantiatedLinkHandler.linkDetails = linkDetails;
    }
    public void SetCurrentActivatedInCaseType(LinkHandler linkHandler) 
    {
        if (currentActiveLink != linkHandler)
        {
            currentActiveLink = linkHandler;
            linkHandler.DeactiveInCaseHighLight();
        }
    }
    public void DecreaseClicked()
    {
        if (currentActiveLink == null)
        {
            return;
        }
        DecreaseInCaseTypeCommand decreaseInCaseTypeCommand = new DecreaseInCaseTypeCommand(currentActiveLink);
        CommandsManager.Instance.ExcuteCommand(decreaseInCaseTypeCommand);
    }
    public void IncreaseClicked()
    {
        if (currentActiveLink == null)
        {
            return;
        }
        InCreaseInCaseTypeCommand decreaseInCaseTypeCommand = new InCreaseInCaseTypeCommand(currentActiveLink);
        CommandsManager.Instance.ExcuteCommand(decreaseInCaseTypeCommand);
    }
    public void CloseGame() 
    {
        HandlLinksData();
        SavingGameStates.Instance.SavePlayerData(LinksManager.Instance.linksData);
        Application.Quit();
    }
    public void ClearList()
    {
        LinksManager.Instance.ClearList();
        foreach (Transform item in dropPanel.GetComponent<ScrollRect>().content.transform)
        {
            Destroy(item.gameObject);
        }
    }
    public void DoScrollOnDrag() 
    {
        Vector2 localpoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(dropPanel, Input.mousePosition, canvas.worldCamera, out localpoint);
        Vector2 normalizedPoint = Rect.PointToNormalized(dropPanel.rect, localpoint);
        float scrollAmount = 0;
        if ((normalizedPoint.x > 0 && normalizedPoint.x < 0.2))
        {
            scrollAmount = -0.005f;
        }
        else if ((normalizedPoint.x > 0.9f && normalizedPoint.x < 1f))
        {
            scrollAmount = 0.005f;
        }
        dropPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition = Mathf.Clamp(dropPanel.GetComponent<ScrollRect>().horizontalNormalizedPosition + scrollAmount, 0, 1);// scrollAmount;
    }
}
