using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainFace{


    Mesh mesh;  //every surface has mesh.
    int resolution;
    Vector3 localUp;    //Every surface's normal vector.
                        //We need to know that. Because we create spheres from cubes.

    Vector3 axisA;
    Vector3 axisB;

    Vector3 position;

    ShapeGenerator shapeGenerator;


    public TerrainFace(Vector3 position, ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 localUp)
    {
        this.mesh = mesh;
        this.resolution = resolution;
        this.localUp = localUp;

        axisA = new Vector3(localUp.y, localUp.z, localUp.x);
        axisB = Vector3.Cross(localUp, axisA);

        this.position = position;

        this.shapeGenerator = shapeGenerator;


    }

    public void constructMesh()
    {
        Vector3[] verticies = new Vector3[resolution * resolution];
        int[] triangles = new int[(int)Mathf.Pow(resolution - 1, 2) * 6];
        //There are (resolution-1)^2 square.
        //Every square has 2 triangle and every triangle has a 3 point.
        int triIndex = 0;


        for (int y = 0; y < resolution; y++)
        {
            for (int x = 0; x < resolution; x++)
            {
                int i = x + y * resolution;
                Vector2 percent = new Vector2(x, y) / (resolution - 1);
                Vector3 pointOnUnitCube = localUp + (percent.x - .5f) * 2 * axisA + (percent.y - .5f) * 2 * axisB;
                Vector3 pointOnUnitSphere = pointOnUnitCube.normalized;
                //verticies[i] = pointOnUnitSphere;
                verticies[i] = shapeGenerator.calculatePointOnPlanet(pointOnUnitSphere, position);


                if(x!= resolution - 1 && y!= resolution - 1)
                {
                    triangles[triIndex] = i;
                    triangles[triIndex + 1] = i + 1 + resolution;
                    triangles[triIndex + 2] = i + resolution;

                    triangles[triIndex + 3] = i;
                    triangles[triIndex + 4] = i + 1;
                    triangles[triIndex + 5] = i + 1 + resolution;

                    triIndex += 6;
                }
            }
        }
        mesh.Clear();

        mesh.vertices = verticies;
        mesh.triangles = triangles;
        mesh.RecalculateNormals();
    }
    
}

