using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public GameObject uiPanel;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            bool isOpen = !uiPanel.activeSelf;
            uiPanel.SetActive(isOpen);

            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;
        }
    }
}