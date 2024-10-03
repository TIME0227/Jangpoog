using UnityEngine;
using UnityEngine.SceneManagement;

public class Silvan : NPC, ITalkable
{
    [SerializeField] private DialogueText dialogueText;
    [SerializeField] private DialogueController dialogueController;

    private void OnEnable()
    {
        // DialogueController�� OnConversationEnd �̺�Ʈ ����
        dialogueController.OnConversationEnd += HandleConversationEnd;
    }

    private void OnDisable()
    {
        // ���� ����
        dialogueController.OnConversationEnd -= HandleConversationEnd;
    }

    public override void Interact()
    {
        Talk(dialogueText);
    }

    public void Talk(DialogueText dialogueText)
    {
        dialogueController.DisplayNextParagraph(dialogueText);
    }

    // ��ȭ ���� ��
    private void HandleConversationEnd()
    {
        // ���� ���� 2�� ����
        Managers.Inventory.invinsibilityCnt += 2;
        Debug.Log(Managers.Inventory.invinsibilityCnt + "��");

        // Scene ��ȯ
        SceneManager.LoadScene("2-1 to demon castle");
    }
}
