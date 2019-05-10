using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class LinkDetails
{
    public LinkTypes linkType;
    public Dictionary<GoToStateTypes, int> gotoNumberOfSteps = new Dictionary<GoToStateTypes, int>();
    public bool allowGOTODisplay;
    public LinkDetails() 
    {
        foreach (GoToStateTypes item in Enum.GetValues(typeof(GoToStateTypes)))
        {
            gotoNumberOfSteps.Add(item, 0);
        }
    }
}
