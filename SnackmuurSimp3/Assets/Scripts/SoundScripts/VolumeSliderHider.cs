using UnityEngine;

public class HideOnStart : MonoBehaviour
{
    public GameObject volumeSliderUI;

    void Start()
    {
        volumeSliderUI.SetActive(false);
    }
}