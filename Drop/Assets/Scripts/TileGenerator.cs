using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{

    public GameObject[] tiles;
    public List<Vector3> tilePositions;
    public string tilesTag;
    public Transform tilesParent;
    public List<int> holePos;
    public GameObject player;


    public int[] types;
    public int colums, rows, seed, levels, levelsDistance = 20;
    private int prevColums, prevRows, prevSeed, prevMaxHoles;
    private float r, c;
    public int maxHoles;

    void Start()
    {
        seed = Random.Range(0, 100);
        SetTilesPositions();
        //GenerateTilesRandomly();
        //MakeHole(0);
        prevColums = colums;
        prevRows = rows;
        prevSeed = seed;
        prevMaxHoles = maxHoles;

        Random.InitState(seed);

        for (int i = 0; i < levels; ++i)
        {
            if (i % 2 == 0)
            {
                //MakeHole(i*15);
                HolesRandomPlace(i * levelsDistance, 1);
            }

            else
                HolesRandomPlace(i * levelsDistance, 2);

        }
    }

    private void Update()
    {
        
        if (prevColums != colums || prevRows != rows || prevSeed != seed || prevMaxHoles != maxHoles)
        {
            DestroyTiles();
            SetTilesPositions();
            //GenerateTilesRandomly();
            for (int i = 0; i < levels; ++i)
            {
                if (i % 2 == 0)
                {
                    //MakeHole(i*15);
                    HolesRandomPlace(i, 1);
                }

                else
                    HolesRandomPlace(i, 2);
            }
            
            prevColums = colums;
            prevRows = rows;
            prevSeed = seed;
            prevMaxHoles = maxHoles;
        }
    }

    //private void LateUpdate()
    //{
    //    tilesParent.rotation = Quaternion.Euler(90f, 0, 0);
    //}

    private void SetTilesPositions()
    {
        int s = 0;

        for (r = 0f; r < rows; ++r)
        {
            for (c = 0f; c < colums; ++c)
            {
                tilePositions.Insert(s, new Vector3(c, 0f, r));
                ++s;
            }
        }
    }

    //public void GenerateTilesRandomly()
    //{
    //    RandomizeType();

    //    int s = 0;

    //    for (int j = 0; j < colums; ++j)
    //    {
    //        for (int k = 0; k < rows; ++k)
    //        {
    //            Instantiate(tiles[types[s]], tilePositions[s], Quaternion.identity);
    //            ++s;
    //        }
    //    }
    //}

    private void DestroyTiles()
    {
        GameObject[] destroyTiles = GameObject.FindGameObjectsWithTag(tilesTag);

        for (int j = 0; j < prevColums * prevRows; ++j)
        {
            Destroy(destroyTiles[j]);
        }
        ClearPositions();
    }

    private void ClearPositions()
    {
        tilePositions.Clear();
    }

    private void MakeHole(int y)
    {
        RandomizeType();
        int index = 0, holeCount = 0;

        for (int j = 0; j < (colums*rows); ++j)
        {
            if (holeCount >= maxHoles)
                types[index] = 0;

            if (types[index] == 1)
            {
                ++holeCount;
            }

            GameObject cube = Instantiate(tiles[types[index]], (tilePositions[index] - new Vector3(0,y,0)), Quaternion.identity);
            cube.transform.parent = tilesParent.transform;
            index++;
        }      
    }

    private void HolesRandomPlace(int y, int color)
    {
        //RandomizeType();

        for (int j = 0; j < maxHoles; ++j)
        {
            int r = Random.Range(0, colums * rows);
            GameObject go1 = Instantiate(tiles[0], (tilePositions[r] - new Vector3(0, y, 0)), Quaternion.identity);
            go1.transform.parent = tilesParent.transform;
            holePos.Add(r);
        }

        for (int i = 0; i < colums*rows; ++i)
        {
            if (holePos.Contains(i))
                continue;
            GameObject go2 = Instantiate(tiles[color], (tilePositions[i] - new Vector3(0, y, 0)), Quaternion.identity);
            go2.transform.parent = tilesParent.transform;
        }
        holePos.Clear();

    }

    private void RandomizeType()
    {
        int x;
        types = new int[colums * rows];
        int i;
        x = Random.Range(1, 100);

        System.Random rnd = new System.Random(x);
        for (i = 0; i < colums * rows; ++i)
        {
            types[i] = rnd.Next(tiles.Length);
        }
    }

}
