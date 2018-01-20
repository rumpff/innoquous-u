using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Sprites;

public class StaticEffect : MonoBehaviour
{

    void Update()
    {
        //Texture2D texture = new Texture2D(128, 128);
        //Sprite sprite = Sprite.Create(texture, new Rect(0, 0, 64, 64), Vector2.zero);
        //GetComponent<SpriteRenderer>().sprite = sprite;

        Texture2D texture = GetComponent<SpriteRenderer>().sprite.texture;

        for (int y = 0; y < texture.height; y++)
        {
            for (int x = 0; x < texture.width; x++) //Goes through each pixel
            {
                Color pixelColour;
                if (Random.Range(0, 2) == 1) //50/50 chance it will be black or white
                {
                    pixelColour = new Color(0, 0, 0, 1);
                }
                else
                {
                    pixelColour = new Color(1, 1, 1, 1);
                }
                texture.SetPixel(x, y, pixelColour);
            }
        }
        texture.Apply();
    }
}
