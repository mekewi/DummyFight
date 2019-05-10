using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangePositionInsideListCommand : Command
{
    public int oldPosition;
    public int newPosition;
    public LinkHandler linkHandler;
    public ChangePositionInsideListCommand(int newPosition, LinkHandler linkHandler) 
    {
        oldPosition = linkHandler.positionInChain;
        this.newPosition = newPosition;
        this.linkHandler = linkHandler;
    }
    public override void Execute()
    {
        linkHandler.transform.SetSiblingIndex(newPosition);
        linkHandler.positionInChain = newPosition;

    }
    public override void Undo()
    {
        base.Undo();
        linkHandler.transform.SetSiblingIndex(oldPosition);
        linkHandler.positionInChain = oldPosition;
    }
}
