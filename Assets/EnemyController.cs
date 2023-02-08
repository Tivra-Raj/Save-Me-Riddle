using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] BoxCollider2D enemyArea;
    [SerializeField] LayerMask layerMask;
    
    [SerializeField] float speed;
    [SerializeField] float startWaitTime;

    float waitTime;
    Vector2 currentPosition;
    Vector2 nextPosition;

    private void Start()
    {
        waitTime = startWaitTime;

        CreatingNextPosition();
    }

    private void Update()
    {
        if (waitTime <= 0f) 
        {
            CreatingNextPosition();
            waitTime = startWaitTime;
        }
        else
        {
            MoveEnemy();
            waitTime -= Time.deltaTime;
        }
    }
    void CreatingNextPosition()
    {
        Bounds bounds = enemyArea.bounds;

        float x = Random.Range(bounds.min.x, bounds.max.x);
        float y = Random.Range(bounds.min.y, bounds.max.y);

        nextPosition = new Vector2(x, y);
    }

    void MoveEnemy()
    {
        currentPosition = transform.position;

        Vector2 direction = nextPosition - currentPosition;

        RaycastHit2D hitInfo = Physics2D.Raycast(currentPosition, direction, 10f, layerMask.value); 
        if(hitInfo.collider != null)
        {
            Debug.DrawRay(transform.position, direction, Color.red);
            CreatingNextPosition();
            
        }
        else
        {
            Debug.DrawRay(transform.position, direction, Color.green);
            transform.position = Vector2.MoveTowards(currentPosition, nextPosition, speed * Time.deltaTime);
        }
    }
}
