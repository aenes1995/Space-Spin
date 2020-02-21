using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour {

    [Range(2, 256)]
    public int resolution = 16;

    public ResourceSettings resourceSettings;

    public ShapeSettings shapeSettings;
    public ColorSettings colorSettings;

    [HideInInspector]
    public bool resourceSettingsFoldout = true;

    [HideInInspector]
    public bool shapeSettingsFoldout = true;

    [HideInInspector]
    public bool colorSettingsFoldout = true;

    public enum FaceRenderMask { All, Top, Bottom, Left, Right, Front, Back }

    public FaceRenderMask faceRenderMask;
    public bool autoUpdate = true;

    [SerializeField, HideInInspector]
    MeshFilter[] meshFilters;
    TerrainFace[] terrainFaces;

    ShapeGenerator shapeGenerator = new ShapeGenerator();
    ColorGenerator colorGenerator = new ColorGenerator();


    public void GeneratePlanet()
    {
        Initialize();
        GenerateMesh();
        GenerateColors();
    }


    private void Initialize()
    {
        
        shapeGenerator.updateSettings(shapeSettings);
        colorGenerator.updateSettings(colorSettings);

        if (meshFilters== null || meshFilters.Length== 0)
        {
            meshFilters = new MeshFilter[6]; //Cubes have 6 surface.         
        }

        terrainFaces = new TerrainFace[6];

        Vector3[] localUpVectors = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

        

        for (int i = 0; i < 6; i++)
        {
            if(meshFilters[i]== null)
            {
                GameObject meshObj = new GameObject("mesh");
                meshObj.transform.position = transform.position;
                meshObj.transform.parent = transform;
                

                meshObj.AddComponent<MeshRenderer>();
                meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                meshFilters[i].sharedMesh = new Mesh();
                
            }
            terrainFaces[i] = new TerrainFace(transform.position, shapeGenerator, meshFilters[i].sharedMesh, resolution, localUpVectors[i]);
            bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
            meshFilters[i].gameObject.SetActive(renderFace); //if this it is false, surface won't render in scene.

            if (meshFilters[i].GetComponent<MeshRenderer>() != null) meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;


        }

    }

    public void OnShapeSettingsUpdated()
    {
        if (autoUpdate)
        {
            Initialize();
            GenerateMesh();
        }

    }


    public void OnColorSettingsUpdated()
    {
        if(autoUpdate)
        {
            Initialize();
            GenerateColors();
        }
    }


    void GenerateMesh()
    {
        for (int i = 0; i < meshFilters.Length; i++)
        {
            if (meshFilters[i].gameObject.activeSelf)
            {
                terrainFaces[i].constructMesh();
            }        
        }

        colorGenerator.updateElevation(shapeGenerator.elevationMinMax);
    }

    void GenerateColors()
    {
        colorGenerator.updateColors();                
    }

    public void refreshTexture()
    {
        colorGenerator.updateSettings(colorSettings);
        colorGenerator.updateColors();
    }

    private void Start()
    {
        refreshTexture();
    }

    private void OnApplicationQuit()
    {
        refreshTexture();
    }

}
