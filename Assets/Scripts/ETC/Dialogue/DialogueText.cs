using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/New Dialogue Container")]
public class DialogueText : ScriptableObject
{
    public Speaker[] speakers;
    [TextArea(5, 10)]
    public string[] paragraphs;
}
