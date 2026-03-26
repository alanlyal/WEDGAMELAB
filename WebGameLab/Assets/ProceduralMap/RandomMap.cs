
using UnityEngine;
using System.Collections.Generic;

public class RandomMap : MonoBehaviour
{
    [SerializeField] private int width;
    [SerializeField] private int depth;
    [SerializeField] private List<GameObject> prefabTilesList = new List<GameObject>();
    [SerializeField] private Transform mapParent;
    [SerializeField] private Transform startPoint;
    [SerializeField] private GameObject[,] map;
    [SerializeField] private List<List<GameObject>> listMap = new List<List<GameObject>>();
    private float xOffset, zOffset;
    [SerializeField] private float perlinScale;

    private void Start()
    {
        map = new GameObject[width,depth];
        zOffset = Random.Range(-1000, -5000);
        xOffset = Random.Range(1000, 5000);
       //BuildRandomMap();
        BuildPerlinNoiseMap();
    }
    private void BuildRandomMap()
    {
        for (int row = 0; row < depth; row++)
        {
            List<GameObject> listRow = new List<GameObject>();
            for (int col = 0; col < width; col++)
            {
                if (row == 0 && col == 0) { continue; }
                Vector3 pos = new Vector3(col * 10f, 0f, row * 10f);
                GameObject tile = Instantiate(prefabTilesList[Random.Range(0, prefabTilesList.Count)], pos, Quaternion.identity, mapParent);
                listRow.Add(tile);
                map[col, row] = tile;
            }
            listMap.Add(listRow);
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            RebuildPerlinMap();
        }
    }
    private void RebuildPerlinMap()
    { 
    }
    private void BuildPerlinNoiseMap()
    {
        for (int row = 0; row < depth; row++)
        {
            List<GameObject> listRow = new List<GameObject>();
            for (int col = 0; col < width; col++)
            {
                if (row == 0 && col == 0) { continue; }
                float perlinNoiseValue = GetPerlinNoise(col, row);
                Vector3 pos = new Vector3(col * 10f, 0f, row * 10f);
                GameObject tile = Instantiate(GenerateTileOnPerlinNoise(perlinNoiseValue),pos, Quaternion.identity, mapParent); 
                listRow.Add(tile);
                map[col, row] = tile;
            }
            listMap.Add(listRow);
        }
    }
    private float GetPerlinNoise(float x, float z)
    {
        // Dividing makes the "features" larger and smoother
        // Try a scale of 0.1f to start
        float scale = 0.1f;
        float xCoord = (x + xOffset) * scale;
        float zCoord = (z + zOffset) * scale;

        return Mathf.PerlinNoise(xCoord, zCoord);
    }
    private GameObject GenerateTileOnPerlinNoise(float noiseValue)
    {
        Debug.Log($"generateTileOnPerlin ({noiseValue})");
        switch (noiseValue)
        {
            case <= 0.2f:
                return prefabTilesList[0];//should be water
            case <= 0.4f:
                 return prefabTilesList[1];//should be grass
            case <= 0.6f:
                return prefabTilesList[2];//should be road
            case <= 0.8f:
                return prefabTilesList[3];//should be ground
            case <= 1f:
                return prefabTilesList[4];//should be lava
                default: return null;//default should be grass
        }
    }
}
