using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<QuestSO, Dictionary<QuestObjective, int>> questProgress = new();


    public void UpdateObjectiveProgress(QuestSO questSO, QuestObjective objective)
    {
        if(!questProgress.ContainsKey(questSO))
            questProgress[questSO] = new Dictionary<QuestObjective, int>();

        var progressDictionary = questProgress[questSO];
        int newAmount = 0;
    }

    public string GetProgressText(QuestSO questSO, QuestObjective objective)
    {
        int currentAmount = 0;

        if(currentAmount >= objective.requiredAmount)
            return "Complete";
        
        else if (objective.targetItem != null)
            return $"{currentAmount}/{objective.requiredAmount}";

        else
            return "In Progress";
    }
}
