using UnityEngine;
using System;

public class Dirt : MonoBehaviour
{
    public event Action OnCleaned;

    private float transparency = 1f;
    public float cleanSpeed = 1f;
    private Renderer rend;
    private QuestHandler questHandler;
    void Start()
    {
        rend = GetComponent<Renderer>();
        rend = GetComponent<Renderer>();
        questHandler = FindAnyObjectByType<QuestHandler>();
    }

    public void Clean()
    {
        
        transparency -= Time.deltaTime * cleanSpeed;
        if (transparency <= 0)
        {
            if (questHandler != null) questHandler.CleanDirt();
            OnCleaned?.Invoke();
            Destroy(gameObject);
        }
    }
}
