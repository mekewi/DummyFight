using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveCommand : Command
{
    GameObject objectToSetActive;
    public SetActiveCommand(GameObject gameObject)
    {
        objectToSetActive = gameObject;
    }
    public override void Execute()
    {
        objectToSetActive.SetActive(true);
    }
    public override void Undo()
    {
        base.Undo();
        objectToSetActive.SetActive(false);
    }
}
