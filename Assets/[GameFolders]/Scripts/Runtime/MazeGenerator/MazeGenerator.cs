using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class MazeGenerator : MonoBehaviour
{
    #region Params
    [SerializeField]
    private MazeCell _mazeCellPrefab;

    [SerializeField]
    private int _mazeWidth;

    [SerializeField]
    private int _mazeDepth;

    [SerializeField]
    private int _seed;

    [SerializeField]
    private bool _useSeed;

    private MazeCell[,] _mazeGrid;

    const float CELL_SIZE=2; //it depends on mazeCellPrefab
    [SerializeField]
    private bool drawGismos;
    #endregion
    public void StartGenerate() 
    {
        DeleteChildCells();
        if (_useSeed)
            Random.InitState(_seed);
        else
        {
            int randomSeed = Random.Range(1, 1000000);
            Random.InitState(randomSeed);

            Debug.Log("RandomSeed="+randomSeed);
        }
        _mazeGrid = new MazeCell[_mazeWidth, _mazeDepth];

        for (int i = 0; i < _mazeWidth; i++)
        {
            for (int k = 0; k < _mazeDepth; k++)
            {
                float xPos = i * CELL_SIZE;
                float zPos = k * CELL_SIZE;
                _mazeGrid[i, k] = Instantiate(_mazeCellPrefab, transform);
                _mazeGrid[i, k].transform.localPosition = new Vector3(xPos, 0, zPos);
            }
        }
        GenerateMaze(null, _mazeGrid[0, 0]);
    }
    private void GenerateMaze(MazeCell previousCell, MazeCell currentCell)
    {
        currentCell.Visit();
        ClearWalls(previousCell, currentCell);

        MazeCell nextCell;

        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);

            if (nextCell != null)
            {
                GenerateMaze(currentCell, nextCell);
            }
        } while (nextCell != null);

    }
    private MazeCell GetNextUnvisitedCell(MazeCell currentCell)
    {
        var unvisitedCells = GetUnvisitedCells(currentCell);

        return unvisitedCells.OrderBy(_ => Random.Range(1, 10)).FirstOrDefault();
    }
    private IEnumerable<MazeCell> GetUnvisitedCells(MazeCell currentCell)
    {
        int x = Mathf.RoundToInt(currentCell.transform.localPosition.x / CELL_SIZE);
        int z = Mathf.RoundToInt(currentCell.transform.localPosition.z / CELL_SIZE);
        if (x + 1 < _mazeWidth)
        {
            var cellToRight = _mazeGrid[x + 1, z];
            if (cellToRight.IsVisited == false)
            {
                yield return cellToRight;
            }
        }
        if (x - 1 >= 0)
        {
            var cellToLeft = _mazeGrid[x - 1, z];

            if (cellToLeft.IsVisited == false)
            {
                yield return cellToLeft;
            }
        }

        if (z + 1 < _mazeDepth)
        {
            var cellToFront = _mazeGrid[x, z + 1];

            if (cellToFront.IsVisited == false)
            {
                yield return cellToFront;
            }
        }
        if (z - 1 >= 0)
        {
            var cellToBack = _mazeGrid[x, z - 1];

            if (cellToBack.IsVisited == false)
            {
                yield return cellToBack;
            }
        }
    }
    private void ClearWalls(MazeCell previousCell, MazeCell currentCell)
    {
        if (previousCell == null)
        {
            return;
        }
        if (previousCell.transform.localPosition.x < currentCell.transform.localPosition.x)
        {
            previousCell.ClearRightWall();
            currentCell.ClearLeftWall();
        }
        if (previousCell.transform.localPosition.x > currentCell.transform.localPosition.x)
        {
            previousCell.ClearLeftWall();
            currentCell.ClearRightWall();
            return;
        }
        if (previousCell.transform.localPosition.z < currentCell.transform.localPosition.z)
        {
            previousCell.ClearFrontWall();
            currentCell.ClearBackWall();
        }
        if (previousCell.transform.localPosition.z > currentCell.transform.localPosition.z)
        {
            previousCell.ClearBackWall();
            currentCell.ClearFrontWall();
            return;
        }

    }
    private void DeleteChildCells()
    {
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
#if UNITY_EDITOR
            DestroyImmediate(transform.GetChild(i).gameObject);
#else
            Destroy(transform.GetChild(i).gameObject);
#endif
        }
    }
    #region Gizmos
    private void OnDrawGizmos()
    {
        if(drawGismos)
            DrawMazeCube();
    }

    private void DrawMazeCube()
    {
        Gizmos.color = Color.green;

        Vector3 startPoint = transform.position;

        for (int i = 0; i < _mazeWidth; i++)
        {
            for (int j = 0; j < _mazeDepth; j++)
            {
                Vector3 cubePosition = startPoint + new Vector3(i * CELL_SIZE, 0f, j * CELL_SIZE);

                Gizmos.DrawWireCube(cubePosition + new Vector3(0, CELL_SIZE*0.5f, 0), new Vector3(CELL_SIZE, CELL_SIZE, CELL_SIZE));
            }
        }
    }
    #endregion
}
