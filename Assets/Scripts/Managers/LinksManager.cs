using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinksManager : Singleton<LinksManager>
{
    public List<LinkDetails> linksData = new List<LinkDetails>();
    void Start()
    {
        DontDestroyOnLoad(gameObject);
    }
    public void LoadLinks() 
    {
        linksData = SavingGameStates.Instance.LoadPlayerData();
        if (linksData.Count > 0)
        {
            GameManager.Instance.LinksLoaded();
        }
    }
    internal void AddLink(LinkDetails linkDetails)
    {
        linksData.Add(linkDetails);
    }
    public List<LinkDetails> GetLinksData(PlayerType playerType) 
    {
        switch (playerType)
        {
            case PlayerType.Player:
                return linksData;
            case PlayerType.Enemy:
                return HandleEnemyLinkData();
            default:
                return new List<LinkDetails>();
        }
    }
    public List<LinkDetails> HandleEnemyLinkData() 
    {
        List<LinkDetails> enemyLinks = new List<LinkDetails>();
        for (int i = 0; i < linksData.Count; i++)
        {
            enemyLinks.Add(new LinkDetails()
            {
                linkType = (LinkTypes)UnityEngine.Random.Range(0, Enum.GetValues(typeof(LinkTypes)).Length),
                gotoNumberOfSteps = new Dictionary<GoToStateTypes, int> {
                { GoToStateTypes.Idle, UnityEngine.Random.Range(0, 100)},
                { GoToStateTypes.Attack, UnityEngine.Random.Range(0, 100)},
                { GoToStateTypes.Dodge, UnityEngine.Random.Range(0, 100)} },
            });
        }
        return enemyLinks;
    }

    internal void ClearList()
    {
        linksData.Clear();
    }
}
