using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemySettings : MonoBehaviour
{
    private WaveManager waveManager;
    private GameManager gameManager;
    private List<Vector2Int> pathCells;
    private int nextPathCellIndex;

    public bool canMove = true;
    public float hp;
    public float speed;
    public float dmg;

    public bool died = false;


    void Start()
    {
        waveManager = GameObject.Find("GameManager").GetComponent<WaveManager>();
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();

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

                transform.position = Vector3.MoveTowards(currentPos, nextPos, Time.deltaTime * speed);

                if(dir != Vector3.zero)
                {
                    transform.forward = dir;
                }


                if (Vector3.Distance(currentPos,nextPos) < 0.1f)
                {
                    if(nextPathCellIndex != 0)
                    {
                        nextPathCellIndex--;
                    }
                    else
                    {
                        if (!died)
                        {
                            died = true;
                            Destroy(gameObject);
                            gameManager?.AddEnemyDestroyed();
                            gameManager?.EnemyAttackedTower(dmg);
                        }

                    }
                }
            }
        }

    }

    public bool AttackedTurret(float dmg)
    {
        hp -= dmg;

        if (hp < 1 && !died)
        {
            died = true;
            Destroy(gameObject);
            gameManager?.AddEnemyDestroyed();
            gameManager?.AddCoins(Random.Range(50,100));
            return true;
        }

        return false;
    }

    public void SetCanMove()
    {
        canMove = true;
    }
}
