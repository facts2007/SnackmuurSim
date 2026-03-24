using UnityEngine;
using System;

public class Dirt : MonoBehaviour
{
    public event Action OnCleaned;

    private float transparency = 1f;
    public float cleanSpeed = 1f;
    private Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    public void Clean()
    {
        
        transparency -= Time.deltaTime * cleanSpeed;
        if (transparency <= 0)
        {
            OnCleaned?.Invoke();
            Destroy(gameObject);
        }
    }
}
