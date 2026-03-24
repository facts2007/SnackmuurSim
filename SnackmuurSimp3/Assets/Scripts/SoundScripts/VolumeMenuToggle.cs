using UnityEngine;

public class ToggleVolumeUI : MonoBehaviour
{
    public GameObject volumeSliderUI;

    public void ToggleUI()
    {
        volumeSliderUI.SetActive(!volumeSliderUI.activeSelf);
    }
}