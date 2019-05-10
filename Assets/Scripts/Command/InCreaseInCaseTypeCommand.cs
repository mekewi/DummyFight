using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InCreaseInCaseTypeCommand : Command
{
    LinkHandler linkHandler;
    GoToStateTypes gamePlayStates;
    public InCreaseInCaseTypeCommand(LinkHandler linkHandler)
    {
        this.linkHandler = linkHandler;
        this.gamePlayStates = linkHandler.currentSelectedInCase;
    }
    public override void Execute()
    {
        linkHandler.IncreaseInCaseTypeValue(gamePlayStates);
    }
    public override void Undo()
    {
        base.Undo();
        linkHandler.DecreaseInCaseTypeValue(gamePlayStates);
    }
}
