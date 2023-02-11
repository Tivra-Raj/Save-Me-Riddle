using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.ReorderableList;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] Player_Controller Player_Controller;
    [SerializeField] BoxCollider2D enemyArea;
    [SerializeField] LayerMask layerMask;
    [SerializeField] Transform target;
    
    [SerializeField] float speed;
    [SerializeField] float startWaitTime;

    float waitTime;
    Vector2 currentPosition;
    Vector2 nextPosition;

    bool playerMoved;

    private void Start()
    {
        waitTime = startWaitTime;

        CreatingNextPosition();
    }

    private void Update()
    {
        if(Player_Controller.movement.sqrMagnitude > 0)
            playerMoved = true;

        Attack();
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

            //flipping the enemy
            Vector2 scale = transform.localScale;
            if (currentPosition.x > nextPosition.x)
            {
                scale.x = 1f * Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = -1f * Mathf.Abs(scale.x);
            }
            transform.localScale = scale;

            transform.position = Vector2.MoveTowards(currentPosition, nextPosition, speed * Time.deltaTime);
        }
    }

    void Attack()
    {
        if (playerMoved == true)
            this.transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        else
        {
            if (waitTime <= 0f && playerMoved == false)
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
    }
}
