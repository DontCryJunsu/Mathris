using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockChild : MonoBehaviour
{
    public BlockParent blockParent;
    [SerializeField] SpriteRenderer spriteRenderer;
    public bool isColor;
    public void ReturnWhite()
    {
        isColor = true;
        Debug.Log("t");
    }

    private void Update()
    {
        if (isColor)
        {
            Color color = spriteRenderer.color;
            color.r = color.r + Time.deltaTime;
            color.g = color.g + Time.deltaTime;
            color.b = color.b+ Time.deltaTime;

            if (color.r > 1 && color.g > 1&&color.b > 1 )
            {
                color.r = 1;
                color.g = 1;
                color.b = 1;
                isColor = false;
            }
            spriteRenderer.color = color;
        }
    }
}
