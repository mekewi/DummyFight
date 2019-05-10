using UnityEngine.UI;
public class InCaseCommand : Command
{
    Text affectedText;
    LinkDetails linkDetails;
    int amount;
    GoToStateTypes gamePlayStates;
    public InCaseCommand(Text affectedText,LinkDetails linkDetails,GoToStateTypes gamePlayStates)
    {
        this.affectedText = affectedText;
        this.linkDetails = linkDetails;
        this.gamePlayStates = gamePlayStates;
    }
    public override void Execute()
    {
        amount++;
        ChangeNumberOfStepsInLinkDetails();
    }
    public void ChangeNumberOfStepsInLinkDetails()
    {
        affectedText.text = amount.ToString();
        linkDetails.gotoNumberOfSteps[gamePlayStates] = amount;
    }
    public override void Undo()
    {
        base.Undo();
        amount --;
        ChangeNumberOfStepsInLinkDetails();
    }
}
