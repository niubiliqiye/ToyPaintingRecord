using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueEvent : MonoBehaviour
{
    public void AddMoney()
    {
        GameManager.Instatic.theNumberOfTimesThePlayerDrawsForMasterQian += 1;
        Money.Instatic.ingotNumber += 20;
    }
}
