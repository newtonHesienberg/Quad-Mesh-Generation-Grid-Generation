using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class MeshGeneration : MonoBehaviour
{
    private MeshFilter mf;

    private Mesh mesh;

    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;

    public int x , y;

    // Start is called before the first frame update
    void Start()
    {
        mf = GetComponent<MeshFilter>();
        mesh = new Mesh();

        StartCoroutine(QuadMeshGenerator());
    }

    private void Update()
    {
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mf.mesh = mesh;
    }


    IEnumerator QuadMeshGenerator()
    {
        vertices = new Vector3[(x + 1) * (y + 1)];

        // for vertices
        for (int k = 0, i = 0; i <= y; i++)
        {
            for (int j = 0; j <= x; j++)
            {
                vertices[k] = new Vector3(j, i, 0);
                k++;
            }
        }
        
        //for triangles
        triangles = new int[x * y * 6];

        int vertexCount = 0;
        int triangleJump = 0;

        for (int i = 0; i < y; i++)
        {
            for (int j = 0; j < x; j++)
            {

                triangles[0 + triangleJump] = vertexCount + 0;
                triangles[1 + triangleJump] = vertexCount + x + 1;
                triangles[2 + triangleJump] = vertexCount + 1;
                triangles[3 + triangleJump] = vertexCount + 1;
                triangles[4 + triangleJump] = vertexCount + x + 1;
                triangles[5 + triangleJump] = vertexCount + x + 2;

                vertexCount++;
                triangleJump += 6;

                yield return new WaitForSeconds(0.1f);
            }

            vertexCount++;
            
        }

        // for uvs
        uvs = new Vector2[vertices.Length];

        
        for (int k = 0 , i = 0; i <= y; i++)
        {
            for (int j = 0; j <= x; j++)
            {
                uvs[k] = new Vector2((float)j / x, (float)i / y);
                k++;
            }
        }

    }

}
