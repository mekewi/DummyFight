using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DecreaseInCaseTypeCommand : Command
{
    LinkHandler linkHandler;
    GoToStateTypes gamePlayStates;
    public DecreaseInCaseTypeCommand(LinkHandler linkHandler)
    {
        this.linkHandler = linkHandler;
        this.gamePlayStates = linkHandler.currentSelectedInCase;
    }
    public override void Execute()
    {
        linkHandler.DecreaseInCaseTypeValue(gamePlayStates);
    }
    public override void Undo()
    {
        base.Undo();
        linkHandler.IncreaseInCaseTypeValue(gamePlayStates);
    }
}
