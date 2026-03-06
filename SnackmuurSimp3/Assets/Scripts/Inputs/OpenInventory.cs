using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public GameObject uiPanel;
    public GameObject ShopUi;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            bool isOpen = !uiPanel.activeSelf;
            uiPanel.SetActive(isOpen);
            ShopUi.SetActive(false);

            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            bool isOpen = !ShopUi.activeSelf;
            ShopUi.SetActive(isOpen);
            uiPanel.SetActive(false);

            Cursor.lockState = isOpen ? CursorLockMode.None : CursorLockMode.Locked;
            Cursor.visible = isOpen;
        }
    }
}