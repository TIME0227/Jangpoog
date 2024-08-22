using UnityEngine;

public class Silvan : NPC, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;
    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        //start convo
        dialogueController.DisplayNextParagraph(dialogueText);

    }
}
