using System;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    public int Columns { get; set; }
    public int Rows { get; set; }
    public GameObject Exit;
    public GameObject[] FloorTiles;
    public GameObject[] EnemyTiles;
    public GameObject[] WallTiles;

    public Count WallCount = new Count(32,32);    

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    public BoardManager()
    {
        Columns = 8;
        Rows = 8;
    }

    public void SetupScene(int level)
    {
        SetupBoard();
        InitializeList();
        //InstantinateObjectsAtRandomPositions(WallTiles, WallCount.Minimum, WallCount.Maximum);
        var enemyCount = (int)Mathf.Log(level, 2);
        InstantinateObjectsAtRandomPositions(EnemyTiles, enemyCount, enemyCount);
        Instantiate(Exit, new Vector3(Columns - 1, Rows - 1, 0), Quaternion.identity);
    }

    void InitializeList()
    {
        gridPositions.Clear();

        for (int x = 1; x < Columns - 1; x++)
        {
            for(int y = 1; y < Rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0));
            }
        }
    }

    void SetupBoard()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = -1; x < Columns + 1; x++)
        {
            for (int y = -1; y < Rows + 1; y++)
            {
                GameObject toInstantiate;
                if (x == -1 || x == Columns || y == -1 || y == Rows)
                {
                    toInstantiate = WallTiles[UnityEngine.Random.Range(0, WallTiles.Length)];
                }
                else
                {
                    toInstantiate = FloorTiles[UnityEngine.Random.Range(0, FloorTiles.Length)];
                }

                var instance = Instantiate(toInstantiate, new Vector3(x, y, 0), Quaternion.identity);
                instance.transform.SetParent(boardHolder);
            }
        }
    }

    Vector3 GetRandomPosition()
    {
        var idx = UnityEngine.Random.Range(0, gridPositions.Count);
        var position =  gridPositions[idx];
        gridPositions.RemoveAt(idx);
        return position;
    }

    void InstantinateObjectsAtRandomPositions(GameObject[] tileArray, int min, int max)
    {
        int objCount = UnityEngine.Random.Range(min, max+1);

        for (int i = 0; i < objCount; i++)
        {
            var position = GetRandomPosition();
            var idx = UnityEngine.Random.Range(0, tileArray.Length);
            var tile = tileArray[idx];
            Instantiate(tile, position, Quaternion.identity);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[Serializable]
public class Count
{
    public int Minimum { get; set; }
    public int Maximum { get; set; }

    public Count(int min, int max)
    {
        Minimum = min;
        Maximum = max;
    }
}
