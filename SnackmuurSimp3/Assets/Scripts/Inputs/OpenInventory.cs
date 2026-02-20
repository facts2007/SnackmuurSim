using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public GameObject uiPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            uiPanel.SetActive(!uiPanel.activeSelf);
        }
    }
}