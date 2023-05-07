using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    private MapPathGenerator pathGenerator;

    public int gridWidth = 30;
    public int gridHeight = 8;
    public int minPathLength = 30;
    public int groundRadiusRange = 3;

    public GameObject baseGate;
    public GameObject enemyGate;
    public GameObject enemyPathGround;
    public GameObject groundCell;

    public Transform enemyPathFolderParent;
    public Transform groundFolderParent;

    public GameObject cameraObject;

    private bool map = false;

    public IEnumerator GenerateMap()
    {
        if(pathGenerator == null)
        {
            pathGenerator = new MapPathGenerator(gridWidth, gridHeight);
            List<Vector2Int> result = PathGenerateWithParameters();
            if(result != null)
            {
                yield return StartCoroutine(StartInitMap(result));
            }
        }
    }

    public bool mapIsDone()
    {
        return map;
    }

    private List<Vector2Int> PathGenerateWithParameters()
    {
        if(pathGenerator != null)
        {
            List<Vector2Int> pathCells = pathGenerator.GeneratePath();

            if(pathCells != null)
            {
                int pathCellsSize = pathCells.Count;

                while (pathCellsSize < minPathLength)
                {
                    pathCells = pathGenerator.GeneratePath();
                    pathCellsSize = pathCells.Count;
                }

                return pathCells;
            }

            return null;
        }

        return null;
    }

    IEnumerator StartInitMap(List<Vector2Int> pathCells)
    {
        yield return InitPathCells(pathCells);
        yield return InitGroundCells();
    }


    IEnumerator InitPathCells(List<Vector2Int> pathCells)
    {
        foreach (Vector2Int cell in pathCells)
        {
            if(pathCells.IndexOf(cell) == 0)
            {
                GameObject clonedObject = Instantiate(baseGate, 
                    new Vector3(
                        cell.y * baseGate.transform.localScale.z, 
                        baseGate.transform.localScale.y - (baseGate.transform.localScale.y / 4f), 
                        cell.x * baseGate.transform.localScale.x + (baseGate.transform.localScale.z/2f - baseGate.transform.localScale.x/2f)
                    ), 
                    baseGate.transform.rotation);
                cameraObject.gameObject.transform.position = new Vector3(cell.y * 10, 125f, cell.x - 35f);
            }
            else if(pathCells.IndexOf(cell) == pathCells.Count - 1)
            {
                GameObject clonedObject = Instantiate(enemyGate,
                    new Vector3(
                        cell.y * 10,
                        enemyGate.transform.localScale.y - (enemyGate.transform.localScale.y / 4f),
                        cell.x * 10 - (enemyGate.transform.localScale.z / 2f + enemyGate.transform.localScale.x / 2f)
                    ),
                    baseGate.transform.rotation);
            }
            else
            {
                GameObject clonedObject = Instantiate(enemyPathGround, new Vector3(cell.y * enemyPathGround.transform.localScale.z, 2f, cell.x * enemyPathGround.transform.localScale.x), Quaternion.identity);
                clonedObject.transform.SetParent(enemyPathFolderParent);
            }

            yield return new WaitForSeconds(0.005f);
        }

        yield return null;
    }

    IEnumerator InitGroundCells()
    {
        for (int y = gridHeight - 1; y >= 0; y--)
        {
            for (int x = 0; x < gridWidth; x++)
            {
                int height = Random.Range(1, 4);
                groundCell.transform.localScale = new Vector3(groundCell.transform.localScale.x, height, groundCell.transform.localScale.z);
                if (CheckNearEnemyPath(x,y))
                {
                    GameObject clonedObject = Instantiate(groundCell, new Vector3(y * groundCell.transform.localScale.x, 2.5f + (height - 1), x * groundCell.transform.localScale.z), Quaternion.identity);
                    clonedObject.transform.SetParent(groundFolderParent);
                    yield return new WaitForSeconds(0.01f);
                }
            }
        }

        yield return null;
    }

    private bool CheckNearEnemyPath(int x, int y)
    {
        if (pathGenerator.CellIsFree(x, y))
        {
            int radius = 1;
            bool found = false;
            while(radius <= groundRadiusRange)
            {
                if(!pathGenerator.CellIsFree(x + radius, y) || 
                   !pathGenerator.CellIsFree(x, y + radius) ||
                   !pathGenerator.CellIsFree(x - radius, y) || 
                   !pathGenerator.CellIsFree(x, y - radius) ||
                   !pathGenerator.CellIsFree(x + radius, y + radius) ||
                   !pathGenerator.CellIsFree(x - radius, y - radius) ||
                   !pathGenerator.CellIsFree(x + radius, y - radius) ||
                   !pathGenerator.CellIsFree(x - radius, y + radius))
                {
                    found = true; break;
                }
                radius++;
            }

            return found;
        }

        return false;
    }

}

public class MapPathGenerator
{
    private int width, height;
    private List<Vector2Int> cells;

    public MapPathGenerator(int width, int height)
    {
        this.width = width;
        this.height = height;
    }

    public List<Vector2Int> GeneratePath()
    {
        cells = new List<Vector2Int>();

        //Kezdõ pozicio
        int y = Random.Range(1, height);

        //Hol tart a hosszusag
        int length = 0;

        //Addig dolgozza fel, ameddig el nem eri el a megadott width-ig
        while (length < width)
        {
            //Hozzaadjuk a cella koordinatat
            cells.Add(new Vector2Int(length, y));

            //Ervenyes-e a mozgas
            bool validMove = false;

            //Addig csinaljon, ameddig nem lesz ervenyes mozgas
            while (!validMove)
            {
                //0 - Fel, 1 - Jobb, 2 - Bal
                int move = Random.Range(0, 3);

                //Ha felfele mozgas tortenik
                if ((move == 0 && length != 0) || length % 2 == 0 || length > (width - 2))
                {
                    length++;
                    validMove = true;
                }
                //Jobb vagy bal iranyba mozgas
                else if ((move == 1 && length % 2 != 0 && y < (height - 2) && CellIsFree(length, y + 1)) || (move == 2 && y > 2 && CellIsFree(length, y - 1)))
                {
                    //Ha a mozgas jobbra tortenik
                    if (move == 1)
                    {
                        y++;
                    }
                    //Ha a mozgas balra tortenik
                    else if (move == 2)
                    {
                        y--;
                    }
                    validMove = true;
                }
            }
        }

        return cells;
    }

    public bool CellIsFree(int x, int y)
    {
        return !cells.Contains(new Vector2Int(x, y));
    }
}