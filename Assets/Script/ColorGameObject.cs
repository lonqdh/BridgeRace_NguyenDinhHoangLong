using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorGameObject : MonoBehaviour
{
    public ColorType colorType;
    public ColorData colorData;

    [SerializeField] private MeshRenderer renderer;

    public void ChangeColor(ColorType colorType)
    {
        this.colorType = colorType;
        renderer.material = colorData.GetColor(colorType);
    }


}
