using Player;
using UnityEngine;

namespace Enemy
{
    public class GhostController
    {
        public GhostView GhostView { get; private set; }
        public GhostModel GhostModel { get; private set; }

        private float waitTime;
        private Vector2 currentPosition;
        private Vector2 nextPosition;

        public bool playerMoved;

        public GhostController(GhostView ghostPrefab, GhostModel ghostModel)
        {
            GhostView = GameObject.Instantiate<GhostView>(ghostPrefab);
            GhostModel = ghostModel;

            GhostView.SetGhostController(this);
            GhostModel.SetGhostController(this);

            waitTime = GhostModel.StartPatrolWaitTime;
        }

        public void CreateNextPosition()
        {
            Bounds bounds = GhostService.Instance.GhostPatrolArea.bounds;

            float x = Random.Range(bounds.min.x, bounds.max.x);
            float y = Random.Range(bounds.min.y, bounds.max.y);

            nextPosition = new Vector2(x, y);
        }

        private void MoveEnemy()
        {
            currentPosition = GhostView.transform.position;

            Vector2 direction = nextPosition - currentPosition;

            RaycastHit2D hitInfo = Physics2D.Raycast(currentPosition, direction, 10f, GhostModel.PlayerLayerMask);
            if (hitInfo.collider != null)
            {
                Debug.DrawRay(GhostView.transform.position, direction, Color.red);
                CreateNextPosition();
            }
            else
            {
                Debug.DrawRay(GhostView.transform.position, direction, Color.green);

                //flipping the enemy
                Vector2 scale = GhostView.transform.localScale;
                if (currentPosition.x > nextPosition.x)
                {
                    scale.x = 1f * Mathf.Abs(scale.x);
                }
                else if(currentPosition.x < nextPosition.x)
                {
                    scale.x = -1f * Mathf.Abs(scale.x);
                }
                GhostView.transform.localScale = scale;

                GhostView.transform.position = Vector2.MoveTowards(currentPosition, nextPosition, GhostModel.Speed * Time.deltaTime);
            }
        }

        public void Patrolling()
        {
            if(playerMoved == false)
            {
                if (waitTime <= 0f)
                {
                    CreateNextPosition();
                    waitTime = GhostModel.StartPatrolWaitTime;
                }
                else
                {
                    MoveEnemy();
                    waitTime -= Time.deltaTime;
                }
            }
        }

        public void Attack()
        {
            if (playerMoved == true)
            {
                GhostView.transform.position = Vector2.MoveTowards(GhostView.transform.position, PlayerService.Instance.PlayerController.GetPlayerPosition(), GhostModel.Speed * Time.deltaTime);
            }
        }
    }
}