using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    private const int m_speed = 30;
    private Vector3 m_setPosition;
    private bool m_bToDestory = false;

    void Start()
    {
        m_setPosition = transform.position;
    }

    public void SetPosition(Vector2 pos)
    {
        m_setPosition = new Vector3(pos.x, pos.y, 0);
    }

    public void SetDestroy()
    {
        m_bToDestory = true;
    }

    void Update()
    {
        if(transform.position != m_setPosition)
        {
            transform.position = Vector3.MoveTowards(transform.position, m_setPosition, Time.deltaTime * m_speed);
        }
        else if(m_bToDestory)
        {
            transform.localScale += new Vector3(-0.1f, -0.1f, -0.1f);

            if(transform.localScale.x < 0.2f)
            {
                Destroy(gameObject);
            }
        }
    }
}
