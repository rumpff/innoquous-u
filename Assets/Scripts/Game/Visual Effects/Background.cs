using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour {

    private float m_power = 3.0f;
    private float m_scale = .6f;
    private Vector2 v2SampleStart = new Vector2(0f, 0f);
    private MeshFilter m_meshFilter;
    Vector3[] m_vertices, m_verticesOriginal;

    private float m_sinNumb;

    void Start()
    {
        m_meshFilter = GetComponent<MeshFilter>();

        v2SampleStart = new Vector2(Random.Range(50.0f, 100.0f), Random.Range(50.0f, 100.0f));
        MakeSomeNoise();
    }

    void Update()
    {
        m_sinNumb += 2 * Time.deltaTime;


        for (int i = 0; i < m_vertices.Length; i++)
        {
            float xCoord = v2SampleStart.x + m_vertices[i].x * m_scale;
            float yCoord = v2SampleStart.y + m_vertices[i].z * m_scale;
            float offset = xCoord + yCoord * 0.5f;

            m_vertices[i].y = m_verticesOriginal[i].y + (Mathf.Sin(m_sinNumb + offset) * 0.01f);
        }
        m_meshFilter.mesh.vertices = m_vertices;
    }

    void MakeSomeNoise()
    {

        m_vertices = m_meshFilter.mesh.vertices;
        m_verticesOriginal = m_vertices;
        for (int i = 0; i < m_vertices.Length; i++)
        {
            float xCoord = v2SampleStart.x + m_vertices[i].x * m_scale;
            float yCoord = v2SampleStart.y + m_vertices[i].z * m_scale;
            m_vertices[i].y = Mathf.PerlinNoise(xCoord, yCoord);//Mathf.Pow(Mathf.PerlinNoise(xCoord, yCoord) - 0.5f, 3) * m_power;
        }
        m_meshFilter.mesh.vertices = m_vertices;
        m_meshFilter.mesh.RecalculateBounds();
        m_meshFilter.mesh.RecalculateNormals();
    }
}