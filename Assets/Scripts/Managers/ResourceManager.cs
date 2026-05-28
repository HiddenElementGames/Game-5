using UnityEngine;
using System.Collections.Generic;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    //private resource declarations
    private float upkeepRequired = 100; //variable to hold the 'win/lose' amount which should be changed at the end of each day.
    private int goldAmount = 0; //holds the users gold amount.
    private int resourceCount = 0; // holds the amount of resources the user has left in each day. 

    void Start()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        //subscribe to events:
        EventManager.StartListening(EventTypes.DayEnd, UpkeepCheck);
        EventManager.StartListening<int>(EventTypes.AddGold, OnAddGold);
    }

    private void OnDisable()
    {
        //unsubscribe to events:
        EventManager.StopListening(EventTypes.DayEnd, UpkeepCheck);
        EventManager.StopListening<int>(EventTypes.AddGold, OnAddGold);
    }


    private void OnResourcePlace()
    {
        resourceCount--;
        
        //check if the user is out of resources for the day and invoke the DayEnd event if true.
        if (resourceCount==0)
        {
            EventManager.Invoke(EventTypes.DayEnd);
        }

    }

    private void UpkeepCheck() //runs at the end of the day to check that the player has the required amount of upkeep to move to the next day.
    {

        if (goldAmount < upkeepRequired)
        {
            Debug.Log("You are a loser.");
        }
    }

    private void OnAddGold(int amount)
    {
        goldAmount += amount;
        EventManager.Invoke(EventTypes.UpdateGoldText, goldAmount);
    }
}