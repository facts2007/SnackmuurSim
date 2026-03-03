using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Scriptable Item")]
public class Item : ScriptableObject
{
    public string Name;
    public TileBase tile;
    public Texture2D image;
    public float Cost;
    public bool stackable = true;
}
