using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LinkHandler : MonoBehaviour
{
    public GameObject gotoDisplay;
    public LinkDetails linkDetails;
    public Text[] InCaseTexts;
    public Image[] InCaseImages;
    public Text MainText;
    public GoToStateTypes currentSelectedInCase;
    public bool isInChainList;
    public int positionInChain;
    void Start()
    {
        DisplayLinkData();
    }
    public void DisplayLinkData() 
    {
        MainText.text = linkDetails.linkType.ToString();
        foreach (GoToStateTypes item in Enum.GetValues(typeof(GoToStateTypes)))
        {
            linkDetails.gotoNumberOfSteps[item] = linkDetails.gotoNumberOfSteps[item];
            InCaseTexts[(int)item].text = linkDetails.gotoNumberOfSteps[item].ToString();
        }
        gotoDisplay.SetActive(linkDetails.allowGOTODisplay);
    }
    public void AddedToList()
    {
        gotoDisplay.SetActive(linkDetails.allowGOTODisplay);
        isInChainList = true;
    }
    public void InCaseButtonClicked(int inCaseTypeIndex)
    {
        GameManager.Instance.SetCurrentActivatedInCaseType(this);
        currentSelectedInCase = (GoToStateTypes)inCaseTypeIndex;
        ActiveInCaseHighLight(currentSelectedInCase);
    }
    public void IncreaseInCaseTypeValue(GoToStateTypes gamePlayStates) 
    {
        linkDetails.gotoNumberOfSteps[gamePlayStates] += 1;
        InCaseTexts[(int)gamePlayStates].text = linkDetails.gotoNumberOfSteps[gamePlayStates].ToString();
    }
    public void DecreaseInCaseTypeValue(GoToStateTypes gamePlayStates)
    {
        linkDetails.gotoNumberOfSteps[gamePlayStates] -= 1;
        InCaseTexts[(int)gamePlayStates].text = linkDetails.gotoNumberOfSteps[gamePlayStates].ToString();
    }

    internal void DeactiveInCaseHighLight()
    {
        foreach (var inCaseImage in InCaseImages)
        {
            inCaseImage.color = Color.white;
        }
    }
    internal void ActiveInCaseHighLight(GoToStateTypes gamePlayStates)
    {
        DeactiveInCaseHighLight();
        InCaseImages[(int)gamePlayStates].color = Color.green;
    }
}
