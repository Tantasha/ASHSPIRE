using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    
    [Header("UI References")]
    public CanvasGroup canvasGroup;
    public TMP_Text actorName;
    public TMP_Text dialogueText;
    public Button[] choiceButtons;
    public Button continueButton; // <-- Add this in the Inspector

    public bool isDialogueActive;

    private DialogueSO currentDialogue;
    private int dialogueIndex;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        // Initially hide UI
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        foreach (var button in choiceButtons)
            button.gameObject.SetActive(false);

        continueButton.gameObject.SetActive(false);
    }

    public void StartDialogue(DialogueSO dialogueSO)
    {
        currentDialogue = dialogueSO;
        dialogueIndex = 0;
        isDialogueActive = true;
        ShowDialogue();
    }

    public void AdvanceDialogue()
    {
        if (dialogueIndex < currentDialogue.lines.Length)
        {
            ShowDialogue();
        }
        else
        {
            ShowChoices();
        }
    }

    private void ShowDialogue()
    {
        DialogueLine line = currentDialogue.lines[dialogueIndex];

        DialogueHistoryTracker.Instance.RecordNPC(line.speaker);
        actorName.text = line.speaker.actorName;
        dialogueText.text = line.text;

        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;

        dialogueIndex++;

        // Enable "Continue" button while showing dialogue lines
        continueButton.gameObject.SetActive(true);
        continueButton.onClick.RemoveAllListeners();
        continueButton.onClick.AddListener(AdvanceDialogue);
    }

    private void ShowChoices()
    {
        ClearChoices();
        continueButton.gameObject.SetActive(false);

        if (currentDialogue.options != null && currentDialogue.options.Length > 0)
        {
            for (int i = 0; i < currentDialogue.options.Length; i++)
            {
                var option = currentDialogue.options[i];
                if (i >= choiceButtons.Length) break;

                choiceButtons[i].GetComponentInChildren<TMP_Text>().text = option.optionText;
                choiceButtons[i].gameObject.SetActive(true);
                choiceButtons[i].onClick.AddListener(() => ChooseOption(option.nextDialogue));
            }
        }
        else
        {
            // Automatically show a single "Continue" option if no choices exist
            choiceButtons[0].GetComponentInChildren<TMP_Text>().text = "Continue";
            choiceButtons[0].gameObject.SetActive(true);
            choiceButtons[0].onClick.AddListener(EndDialogue);
        }
    }

    private void ChooseOption(DialogueSO nextDialogue)
    {
        if (nextDialogue == null)
        {
            EndDialogue();
        }
        else
        {
            ClearChoices();
            StartDialogue(nextDialogue);
        }
    }

    private void EndDialogue()
    {
        dialogueIndex = 0;
        isDialogueActive = false;
        ClearChoices();

        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;

        continueButton.gameObject.SetActive(false);
    }

    private void ClearChoices()
    {
        foreach (var button in choiceButtons)
        {
            button.gameObject.SetActive(false);
            button.onClick.RemoveAllListeners();
        }
    }
}
