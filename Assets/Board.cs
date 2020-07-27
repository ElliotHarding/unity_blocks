using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Board : MonoBehaviour
{
    GameObject[,] m_board;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("up"))
        {
            m_bRequestSpawn = true;
        }
        else if (Input.GetKeyDown("down"))
        {
            m_bRequestSpawn = true;
        }
        else if (Input.GetKeyDown("left"))
        {
            m_bRequestSpawn = true;
        }
        else if (Input.GetKeyDown("right"))
        {
            m_bRequestSpawn = true;
        }
    }

    bool SpawnNew()
    {
        m_bRequestSpawn = false;

        if(m_board[0,0] == null)
        {
            m_board[0,0] = Instantiate(blockPrefabA, new Vector2(-4,4), Quaternion.identity)
        }
        else if(m_board[0,1] == null)
        {
            m_board[0,1] = Instantiate(blockPrefabA, new Vector2(4,4), Quaternion.identity)
        }
        else if(m_board[0,2] == null)
        {
            m_board[0,2] = Instantiate(blockPrefabA, new Vector2(4,-4), Quaternion.identity)
        }
        else if(m_board[1,0] == null)
        {
            m_board[1,0] = Instantiate(blockPrefabA, new Vector2(-4,-4), Quaternion.identity)
        }
        else if(m_board[1,1] == null)
        {
            m_board[1,1] = Instantiate(blockPrefabA, new Vector2(0,0), Quaternion.identity)
        }
        else if(m_board[1,2] == null)
        {
            m_board[1,2] = Instantiate(blockPrefabA, new Vector2(4,0), Quaternion.identity)
        }
        else if(m_board[2,0] == null)
        {
            m_board[2,0] = Instantiate(blockPrefabA, new Vector2(0,4), Quaternion.identity)
        }
        else if(m_board[2,1] == null)
        {
            m_board[2,1] = Instantiate(blockPrefabA, new Vector2(-4,0), Quaternion.identity)
        }
        else if(m_board[2,2] == null)
        {
            m_board[2,2] = Instantiate(blockPrefabA, new Vector2(0,-4), Quaternion.identity)
        }
        else
        {
            return false;
        }

        //Spawn new block
        if (Random.Range(0, 2) == 1)//todo
        {
            GameObject newInstance = Instantiate(blockPrefabA, newLocation, Quaternion.identity);
            m_board.Add(newInstance);
        }
        else
        {
            GameObject newInstance = Instantiate(blockPrefabB, newLocation, Quaternion.identity);
            m_board.Add(newInstance);
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
