using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ColorType { None = 0, Green = 1, Blue =2, Red = 3, Yellow = 4}
[CreateAssetMenu(menuName = "ScriptableObjects/ColorData")]
public class ColorData : ScriptableObject
{
    public List<Material> materials;
    public Material GetColor(ColorType color)
    {
        return materials[(int)color];
    }



}

