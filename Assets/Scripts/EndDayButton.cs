using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndDayButton : MonoBehaviour
{
    private const int SHOP_SCENE_INDEX = 1;

    private void Start()
    {
        GetComponent<Button>().onClick.AddListener(EndDay);
    }

    private void EndDay()
    {
        SceneManager.LoadScene(SHOP_SCENE_INDEX);
    }
}
