using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueController : MonoBehaviour
{
  [SerializeField] private TextMeshProUGUI NPCNameText;
  [SerializeField] private TextMeshProUGUI NPCDialogueText;
  [SerializeField] private float typeSpeed = 15;
  private Queue<string> paragraphs = new Queue<string>();


  private bool conversationEnded;
  private bool isTyping;
  private bool dialoguePlayed;

  private int num = 0;
  
  private string p;

  private Coroutine typeDialogueCoroutine;

  private const string HTML_ALPHA = "<color=#00000000>";
  private const float MAX_TYPE_TIME = 0.1f;
  
  public void DisplayNextParagraph(DialogueText dialogueText)
  {
    if (dialoguePlayed)
    {
      gameObject.SetActive(false);
      return;
    }
      //if there is nothing in the queue
          if (paragraphs.Count == 0)
          {
            if (!conversationEnded)
            {
              //start a conversation
              StartConversation(dialogueText);
            }
      
            else if (conversationEnded && !isTyping)
            {
              //end a conversation
              EndConversation();
              return;
            }
          }

          

    
    
    //If there is something in the queue
    if (!isTyping)
    {
      p = paragraphs.Dequeue();
      typeDialogueCoroutine = StartCoroutine(TypeDialogueText(p));
    }
    
    //conversation is being typed out
    else
    {
      FinishParagraphEarly();
    }


    //update conversationEnded bool
    if (paragraphs.Count == 0)
    {
      conversationEnded = true;
      dialoguePlayed = true;
    }

  }

  private void StartConversation(DialogueText dialogueText)
  {
    //activate gameObject
    if (!gameObject.activeSelf)
    {
      gameObject.SetActive(true);
      
    }
    
    //upate the name
    NPCNameText.text = dialogueText.speakerName;
    
    //add dialogue text to the queue
    for (int i = 0; i < dialogueText.paragraphs.Length ; i++)
    {
      paragraphs.Enqueue(dialogueText.paragraphs[i]);
    }

  }

  private void EndConversation()
  {
    //clear the queue
    paragraphs.Clear();
    //return bool to false
    conversationEnded = false;
    
    //deactivate gameobject
    if (gameObject.activeSelf)
    {
      gameObject.SetActive(false);
    }
  }

  private IEnumerator TypeDialogueText(string p)
  {
    isTyping = true;

    NPCDialogueText.text = "";

    string originalText = p;
    string displayedText = "";
    int alphaIndex = 0;
    foreach (char c in p.ToCharArray())
    {
      alphaIndex++;
      NPCDialogueText.text = originalText;

      displayedText = NPCDialogueText.text.Insert(alphaIndex, HTML_ALPHA);

      NPCDialogueText.text = displayedText;

      yield return new WaitForSeconds(MAX_TYPE_TIME / typeSpeed);

    }

    isTyping = false;
  }

  private void FinishParagraphEarly()
  {
    //stop the coroutine
    StopCoroutine(typeDialogueCoroutine);
    
    //finish displaying the text
    NPCDialogueText.text = p;
    
    //update isTyping bool
    isTyping = false;

  }
}
