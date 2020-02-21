using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class NoiseSettings{

    public enum FilterType { Simple, Rigid };
    public FilterType filterType;

    [ConditionalHide("filterType", 0)]
    public SimpleNoiseSettings simpleNoiseSettings;

    [ConditionalHide("filterType", 1)]
    public RigidNoiseSettings rigidNoiseSettings;


    
    [System.Serializable]
    public class SimpleNoiseSettings
    {
        public float strenght = 1;
        public float baseRoughness = 1;
        public float roughness = 2;
        public Vector3 center;
        [Range(1, 8)]

        public int layerCount = 1;      //how many noise layer?
        public float persistance = 0.5f;    //for setting amplitude of noise

        public float minValue; //if noise value smaller than this it becomes zero.                               
    }

    
    [System.Serializable]
    public class RigidNoiseSettings : SimpleNoiseSettings
    {
        public float weightMultiplier = 0.8f;
    }
    

}
