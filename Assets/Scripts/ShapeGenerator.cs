using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShapeGenerator{


    public MinMax elevationMinMax;

    ShapeSettings settings;
    INoiseFilter[] noiseFilters;  

    public void updateSettings(ShapeSettings settings)
    {
        if(settings!= null)
        {
            this.settings = settings;
            noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
            for (int i = 0; i < noiseFilters.Length; i++)
            {
                noiseFilters[i] = NoiseFilterFactory.createNoiseFilter(settings.noiseLayers[i].noiseSettings);
            }
            elevationMinMax = new MinMax();
        }
    }

    public Vector3 calculatePointOnPlanet(Vector3 pointOnUnitSphere, Vector3 position)
    {
        float elevation = 0;
        float firstLayerValue = 0;

        if (noiseFilters.Length > 0)
        {
            firstLayerValue = noiseFilters[0].Evaluate(pointOnUnitSphere);
            if (settings.noiseLayers[0].enabled) elevation = firstLayerValue;
        }

        for (int i = 1; i < noiseFilters.Length; i++)
        {
            if (settings.noiseLayers[i].enabled)
            {
                float mask = (settings.noiseLayers[i].useFirstLayerAsMask) ? firstLayerValue : 1;
                elevation += noiseFilters[i].Evaluate(pointOnUnitSphere) * mask;
            }
            
        }
        elevation = settings.planetRadius * (1 + elevation);
        elevationMinMax.addValue(elevation);
        return pointOnUnitSphere * elevation;      
    }

}
