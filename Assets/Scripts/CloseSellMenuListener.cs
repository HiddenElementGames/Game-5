using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class CloseSellMenuListener : MonoBehaviour
{
    private void Update()
    {
        if(Mouse.current.leftButton.wasReleasedThisFrame)
        {
            StartCoroutine(CloseMenu());
        }
    }

    private IEnumerator CloseMenu()
    {
        yield return null;
        Destroy(gameObject);
    }
}
