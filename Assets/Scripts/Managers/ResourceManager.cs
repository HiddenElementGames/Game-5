using UnityEngine;
using System.Collections.Generic;


/*
TODO: implement the call of a 'resource placed' so that the "DayEnd" event is called upon reaching zero resources left. 


 */


public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    //private resource declarations
    private float upkeepRequired = 100; //variable to hold the 'win/lose' amount which should be changed at the end of each day.
    private float goldAmount = 0; //holds the users gold amount.
    private int resourceCount = 0; // holds the amount of resources the user has left in each day. 

    void Start()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        //subscribe to events:
        EventManager.StartListening(EventTypes.DayEnd, UpkeepCheck);

    }

    private void OnDisable()
    {
        //unsubscribe to events:
        EventManager.StopListening(EventTypes.DayEnd, UpkeepCheck);

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



}


/*
 
This code was taken from Blockor, but I don't think we'll need a dictionary immediately. This could be implemented later on if 
we choose to add more in, for now, just going to make a variable and track the players 'Gold' in there.
-D
  
public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

        // dictionary code taken from Blockor, modified for game 5.

    private Dictionary<ResourceType, float> resourceAmounts =
        new Dictionary<ResourceType, float>
        {
            { ShopGold, 0 }
        };

    private float upkeepRequired = 100; //variable to hold the 'win/lose' amount which should be changed at the end of each day.

    void Start()
    {
        Instance = this;
    }

}
*/