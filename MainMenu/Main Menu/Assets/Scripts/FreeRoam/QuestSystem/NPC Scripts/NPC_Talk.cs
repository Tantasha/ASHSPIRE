using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_Talk : MonoBehaviour
{
    private Rigidbody2D rb;
    public Animator interactAnim;

    public List<DialogueSO> conversations;
    public DialogueSO currentConversation;
    

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        rb.velocity = Vector2.zero;
        rb.isKinematic = true;
        interactAnim.Play("Open");
    }

    private void OnDisable()
    {
        interactAnim.Play("Close");
    }

    private void Update()
    {
        if(Input.GetButtonDown("Submit"))
        {
            if(DialogueManager.Instance.isDialogueActive)
                DialogueManager.Instance.AdvanceDialogue();
            else 
            {
                CheckForNewConversation();
                DialogueManager.Instance.StartDialogue(currentConversation);
            }
        }
    }
    
    private void CheckForNewConversation()
    {
        for(int i = 0; i < conversations.Count; i++)
        {
            var convo = conversations[i];
            if(convo != null && convo.IsConditionMet())
            {
                conversations.RemoveAt(i);
                currentConversation = convo;
                break;
            }
        }
    }
}
