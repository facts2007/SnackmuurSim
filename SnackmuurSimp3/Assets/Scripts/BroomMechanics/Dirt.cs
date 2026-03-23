using UnityEngine;

public class Dirt : MonoBehaviour
{
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
            Destroy(gameObject);
        }
        else
        {
            Color c = rend.material.color;
            c.a = transparency;
            rend.material.color = c;
        }
    }
}