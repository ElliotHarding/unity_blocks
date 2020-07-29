using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Board : MonoBehaviour
{
    GameObject[,] m_board = new GameObject[3,3];
    Vector2[,] m_boardPositions = new Vector2[3,3];

    public GameObject blockPrefabA;
    public GameObject blockPrefabB;
    public Text m_txtLevel;
    private int m_level = 0;

    private Vector2 firstPressPos;
    private Vector2 secondPressPos;
    private Vector2 currentSwipe;

    // Start is called before the first frame update
    void Start()
    {
        m_boardPositions[0,0] = new Vector2(-3.27f,3.34f);
        m_boardPositions[1,0] = new Vector2(0,3.34f);
        m_boardPositions[2,0] = new Vector2(3.27f,3.34f);
        m_boardPositions[0,1] = new Vector2(-3.27f,0);
        m_boardPositions[1,1] = new Vector2(0,0);
        m_boardPositions[2,1] = new Vector2(3.27f,0);
        m_boardPositions[0,2] = new Vector2(-3.27f,-3.34f);
        m_boardPositions[1,2] = new Vector2(0,-3.34f);
        m_boardPositions[2,2] = new Vector2(3.27f,-3.34f);

        m_board[1,1] = Instantiate(blockPrefabA, m_boardPositions[1,1], Quaternion.identity);
        m_level = 0;
        m_txtLevel.text = "" + m_level;
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

    void incLevel()
    {
        m_level++;
        m_txtLevel.text = "" + m_level;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown("up"))
        {
            MovementAnimationUpDown(true);
            MovementAnimationUpDown(true);
            SpawnNew();
            CheckDelete();
            incLevel();
        }
        else if (Input.GetKeyDown("down"))
        {
            MovementAnimationUpDown(false);
            MovementAnimationUpDown(false);
            SpawnNew();
            CheckDelete();
            incLevel();
        }
        else if (Input.GetKeyDown("left"))
        {
            MovementAnimationLeftRight(true);
            MovementAnimationLeftRight(true);
            SpawnNew();
            CheckDelete();
            incLevel();
        }
        else if (Input.GetKeyDown("right"))
        {
            MovementAnimationLeftRight(false);
            MovementAnimationLeftRight(false);
            SpawnNew();
            CheckDelete();
            incLevel();
        }

        if(Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if(t.phase == TouchPhase.Began)
            {
                //save began touch 2d point
                firstPressPos = new Vector2(t.position.x,t.position.y);
            }

            if(t.phase == TouchPhase.Ended)
            {
                secondPressPos = new Vector2(t.position.x,t.position.y);  
                currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
                currentSwipe.Normalize();

                if(currentSwipe.y > 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    MovementAnimationUpDown(true);
                    MovementAnimationUpDown(true);
                    SpawnNew();
                    CheckDelete();
                    incLevel();
                }

                if(currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f)
                {
                    MovementAnimationUpDown(false);
                    MovementAnimationUpDown(false);
                    SpawnNew();
                    CheckDelete();
                    incLevel();
                }

                if(currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    MovementAnimationLeftRight(true);
                    MovementAnimationLeftRight(true);
                    SpawnNew();
                    CheckDelete();
                    incLevel();
                }

                if(currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f)
                {
                    MovementAnimationLeftRight(false);
                    MovementAnimationLeftRight(false);
                    SpawnNew();
                    CheckDelete();
                    incLevel();
                }
            }
        }
    }

    void MovementAnimationUpDown(bool up)
    {
        int dir;
        int start;
        int max;
        int min;
        if (up)
        {
            dir = 1;
            start = 0;
            max = 2;
            min = -1;
        }
        else
        {
            dir = -1;
            start = 2;
            max = 3;
            min = 0;
        }
        
        for(int y = start; y > min && y < max; y+=dir)
        {
            for(int x = 0; x < 3; x++)
            {
                if(m_board[x,y] == null && m_board[x,y+dir] != null)
                {
                    m_board[x,y] = m_board[x,y+dir];
                    m_board[x,y+dir] = null;

                    //todo animaiton
                    if(m_board[x,y] != null)
                        m_board[x,y].GetComponent<Block>().SetPosition(m_boardPositions[x,y]);
                }
            }
        }
    }

    void MovementAnimationLeftRight(bool left)
    {
        int dir;
        int start;
        int max;
        int min;
        if (left)
        {
            dir = 1;
            start = 0;
            min = -1;
            max = 2;
        }
        else
        {
            dir = -1;
            start = 2;
            min = 0;
            max = 3;
        }
        
        for(int x = start; x > min && x < max; x+=dir)
        {
            for(int y = 0; y < 3; y++)
            {                
                if(m_board[x,y] == null && m_board[x+dir,y] != null)
                {
                    m_board[x,y] = m_board[x+dir,y];
                    m_board[x+dir,y] = null;

                    //todo animaiton
                    if(m_board[x,y] != null)
                        m_board[x,y].GetComponent<Block>().SetPosition(m_boardPositions[x,y]);
                }
            }
        }
    }

    bool SpawnNew()
    {
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
            m_board[0,0] = Instantiate(prefab, m_boardPositions[0,0], Quaternion.identity);
        }
        else if(m_board[0,2] == null)
        {
            m_board[0,2] = Instantiate(prefab, m_boardPositions[0,2], Quaternion.identity);
        }
        else if(m_board[2,0] == null)
        {
            m_board[2,0] = Instantiate(prefab, m_boardPositions[2,0], Quaternion.identity);
        }
        else if(m_board[2,2] == null)
        {
            m_board[2,2] = Instantiate(prefab, m_boardPositions[2,2], Quaternion.identity);
        }
        else if(m_board[0,1] == null)
        {
            m_board[0,1] = Instantiate(prefab, m_boardPositions[0,1], Quaternion.identity);
        }        
        else if(m_board[1,0] == null)
        {
            m_board[1,0] = Instantiate(prefab, m_boardPositions[1,0], Quaternion.identity);
        }
        else if(m_board[1,2] == null)
        {
            m_board[1,2] = Instantiate(prefab, m_boardPositions[1,2], Quaternion.identity);
        }
        else if(m_board[2,1] == null)
        {
            m_board[2,1] = Instantiate(prefab, m_boardPositions[2,1], Quaternion.identity);
        }
        else if(m_board[1,1] == null)
        {
            m_board[1,1] = Instantiate(prefab, m_boardPositions[1,1], Quaternion.identity);
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
                m_board[0, y].GetComponent<Block>().SetDestroy();
                m_board[1, y].GetComponent<Block>().SetDestroy();
                m_board[2, y].GetComponent<Block>().SetDestroy();
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
                m_board[x, 0].GetComponent<Block>().SetDestroy();
                m_board[x, 1].GetComponent<Block>().SetDestroy();
                m_board[x, 2].GetComponent<Block>().SetDestroy();
                m_board[x, 0] = null;
                m_board[x, 1] = null;
                m_board[x, 2] = null;
            }
        }
    }

    public void Refresh()
    {
        for(int x = 0; x < 3; x++)
        {
            for (int y = 0; y < 3; y++)
            {
                if(m_board[x, y] != null)
                {
                    m_board[x, y].GetComponent<Block>().SetDestroy();
                    m_board[x, y] = null;
                }
            }
        }

        m_board[1,1] = Instantiate(blockPrefabA, m_boardPositions[1,1], Quaternion.identity);
        m_level = 0;
        m_txtLevel.text = "" + m_level;
    }
}
