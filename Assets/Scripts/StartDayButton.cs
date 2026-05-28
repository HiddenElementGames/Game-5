using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartDayButton : MonoBehaviour
{
    private const int GAME_SCENE_INDEX = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(StartDay);
    }

    private void StartDay()
    {
        SceneManager.LoadScene(GAME_SCENE_INDEX);
    }
}
