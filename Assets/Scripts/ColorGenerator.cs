using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ColorGenerator{

    ColorSettings colorSettings;
    Texture2D texture;
    const int textureResolution = 50;
    INoiseFilter biomeNoiseFilter;

    public void updateSettings(ColorSettings colorSettings)
    {
        this.colorSettings = colorSettings;
        if(texture == null)
        {
            //this.texture = new Texture2D(textureResolution, 1);
            this.texture = new Texture2D(textureResolution, 1, TextureFormat.RGBA32, false);
        }
        
    }
    public void updateElevation(MinMax elevationMinMax)
    {
        colorSettings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationMinMax.min, elevationMinMax.max));
    }


    public void updateColors()
    {
        Color[] textureColors = new Color[textureResolution];

            for (int i = 0; i < textureResolution ; i++)
            {
                textureColors[i] = colorSettings.gradient.Evaluate(i / (textureResolution - 1f));
            }


        texture.SetPixels(textureColors);
        texture.Apply();
        colorSettings.planetMaterial.SetTexture("_texture", texture);
    }
}
