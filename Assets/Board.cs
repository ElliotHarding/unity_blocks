using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    GameObject[,] m_board = new GameObject[3,3];
    public GameObject blockPrefabA;
    public GameObject blockPrefabB;
    private bool m_bRequestSpawn = false;

    // Start is called before the first frame update
    void Start()
    {
        m_board[1,1] = Instantiate(blockPrefabA, new Vector2(-4,4), Quaternion.identity);
    }

    void LogBoard()
    {
        for(int x = 0; x < 3; x++)
        {
            for(int y = 0; y < 3; y++)
            {
                Debug.Log(x + " " + y + m_board[x,y]);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("up"))
        {
            LogBoard();
            m_bRequestSpawn = true;
            MovementAnimationUpDown(true);
            LogBoard();
        }
        else if (Input.GetKeyDown("down"))
        {
            m_bRequestSpawn = true;
            MovementAnimationUpDown(false);
        }
        else if (Input.GetKeyDown("left"))
        {
            m_bRequestSpawn = true;
            MovementAnimationLeftRight(true);
        }
        else if (Input.GetKeyDown("right"))
        {
            m_bRequestSpawn = true;
            MovementAnimationLeftRight(false);
        }
    }

    void MovementAnimationUpDown(bool up)
    {
        int dir;
        int start;
        if (up)
        {
            dir = 1;
            start = 0;
        }
        else
        {
            dir = -1;
            start = 2;
        }
        
        for(int y = start; y > -1 && y < 3; y+=dir)
        {
            for(int x = 0; x < 3; x++)
            {
                if(y == 2 && dir == 1)
                {
                    break;
                }
                if(m_board[x,y] == null)
                {
                    m_board[x,y] = m_board[x,y+dir];
                    m_board[x,y+dir] = null;

                    //todo animaiton

                }
            }
        }
    }

    void MovementAnimationLeftRight(bool left)
    {
        int dir;
        int start;
        if (left)
        {
            dir = 1;
            start = 0;
        }
        else
        {
            dir = -1;
            start = 2;
        }
        
        for(int x = start; x > -1 && x < 3; x+=dir)
        {
            for(int y = 0; y < 3; y++)
            {
                if(x == 2 && dir == 1)
                {
                    break;
                }
                if(m_board[x,y] == null)
                {
                    m_board[x,y] = m_board[x+dir,y];
                    m_board[x+dir,y] = null;

                    //todo animaiton

                }
            }
        }
    }

    bool SpawnNew()
    {
        m_bRequestSpawn = false;

        GameObject prefab;
        if (Random.Range(0, 2) == 1)
        {
            prefab = blockPrefabA;
        }
        else
        {
            prefab = blockPrefabB;
        }

        if(m_board[0,0] == null)
        {
            m_board[0,0] = Instantiate(prefab, new Vector2(-4,4), Quaternion.identity);
        }
        else if(m_board[0,1] == null)
        {
            m_board[0,1] = Instantiate(prefab, new Vector2(4,4), Quaternion.identity);
        }
        else if(m_board[0,2] == null)
        {
            m_board[0,2] = Instantiate(prefab, new Vector2(4,-4), Quaternion.identity);
        }
        else if(m_board[1,0] == null)
        {
            m_board[1,0] = Instantiate(prefab, new Vector2(-4,-4), Quaternion.identity);
        }
        else if(m_board[1,1] == null)
        {
            m_board[1,1] = Instantiate(prefab, new Vector2(0,0), Quaternion.identity);
        }
        else if(m_board[1,2] == null)
        {
            m_board[1,2] = Instantiate(prefab, new Vector2(4,0), Quaternion.identity);
        }
        else if(m_board[2,0] == null)
        {
            m_board[2,0] = Instantiate(prefab, new Vector2(0,4), Quaternion.identity);
        }
        else if(m_board[2,1] == null)
        {
            m_board[2,1] = Instantiate(prefab, new Vector2(-4,0), Quaternion.identity);
        }
        else if(m_board[2,2] == null)
        {
            m_board[2,2] = Instantiate(prefab, new Vector2(0,-4), Quaternion.identity);
        }
        else
        {
            return false;
        }

        return true;
    }

    void CheckDelete()
    {       
        //Check across
        for (int y = 0; y < 3; y++)
        {
            if(m_board[0, y] != null && m_board[1, y] != null && m_board[2, y] &&
            m_board[0,y].tag == m_board[1, y].tag && m_board[1, y].tag == m_board[2, y].tag
            )
            {
                Destroy(m_board[0, y]);
                Destroy(m_board[1, y]);
                Destroy(m_board[2, y]);
                m_board[0, y] = null;
                m_board[1, y] = null;
                m_board[2, y] = null;
            }
        }

        //check down
        for(int x = 0; x < 3; x++)
        {
            if(m_board[x, 0] != null && m_board[x, 1] != null && m_board[x, 2] &&
            m_board[x,0].tag == m_board[x, 1].tag && m_board[x, 1].tag == m_board[x, 2].tag
            )
            {
                Destroy(m_board[x, 0]);
                Destroy(m_board[x, 1]);
                Destroy(m_board[x, 2]);
                m_board[x, 0] = null;
                m_board[x, 1] = null;
                m_board[x, 2] = null;
            }
        }
    }
}
