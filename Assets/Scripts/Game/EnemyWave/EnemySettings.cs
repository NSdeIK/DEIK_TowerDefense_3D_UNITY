using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySettings : MonoBehaviour
{
    private WaveManager waveManager;
    public int nextPathCellIndex;
    public bool canMove = true;
    public List<Vector2Int> pathCells;

    void Start()
    {
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();
        if(waveManager?.getEnemyPathCells() != null)
        {
            pathCells = waveManager.getEnemyPathCells();
            nextPathCellIndex = pathCells.Count - 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            if (waveManager != null && pathCells != null && pathCells.Count > 0)
            {
                Vector3 currentPos = transform.position;
                Vector3 nextPos = new Vector3(pathCells[nextPathCellIndex].y * 10, gameObject.transform.position.y, pathCells[nextPathCellIndex].x * 10);
                Vector3 dir = nextPos - transform.position;

                transform.position = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime * 10f);
                transform.forward = dir;

                if (Vector3.Distance(currentPos,nextPos) < 0.1f)
                {
                    if(nextPathCellIndex != 0)
                    {
                        nextPathCellIndex--;
                    }
                    else
                    {
                        Destroy(gameObject);
                        //tower hp
                    }
                }
            }
        }

    }

    public void SetCanMove()
    {
        canMove = true;
    }
}
