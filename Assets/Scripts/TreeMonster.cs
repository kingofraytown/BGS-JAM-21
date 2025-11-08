using UnityEngine;

public class TreeMonster : MonoBehaviour
{

    public float speed;
    public GameFloat gSpeed;
    public Transform player;
    public float attackRange;
    public float alertRange;
    public float playerOffset = 10f;
    public float distance;
    public Animator animator;
    public bool chase = false;
    public float xTarget;
    public SpriteRenderer sr;
    public bool isRight = true;
    public float treeDistance;

    public enum treeState
    {
        idle = 0,
        awake,
        alert,
        attack,
        hit,
        dead
    }

    public treeState currentState = treeState.idle;

    public void ChangeState(treeState g)
    {
        switch (g)
        {
            case treeState.awake:
                animator.SetInteger("state", 1);
                if (isRight)
                {
                    sr.flipX = false;
                } else
                {
                    sr.flipX = true;
                }
                break;
            case treeState.alert:
                animator.SetInteger("state", 2);
                break;
            case treeState.attack:
                animator.SetInteger("state", 3);
                break;
            case treeState.hit:
                animator.SetInteger("state", 4);
                break;
        }

        currentState = g;
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (transform.position.x > 0f)
        {
            isRight = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        distance = Mathf.Abs(Vector3.Distance(this.transform.position, player.position));

        if (currentState == treeState.idle)
        {
            if (distance <= alertRange)
            {
                ChangeState(treeState.awake);
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (-1 * gSpeed.value() * Time.deltaTime));
        }

        else if (currentState == treeState.awake)
        {
            //move to xTarget
            int dir = 1;
            if (isRight)
            {
                dir = -1;
            }

            transform.position = new Vector3(transform.position.x + (dir * speed * Time.deltaTime), transform.position.y, transform.position.z + (-1 * gSpeed.value() * Time.deltaTime));

            //check if tree monster reached the spot
            treeDistance = Mathf.Abs(transform.position.x - xTarget);

            if(treeDistance < 0.25)
            {
                ChangeState(treeState.alert);
            }
            
        }

        else if (currentState == treeState.alert)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (-1 * gSpeed.value() * Time.deltaTime));
            if (distance <= attackRange)
            {
                ChangeState(treeState.attack);
            }
        }
        
        else if(currentState == treeState.attack)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z + (-1 * gSpeed.value() * Time.deltaTime));
        }

        if (transform.position.z < -30f)
        {
            gameObject.SetActive(false);
        }
    }

    public void Hit()
    {
        if (currentState != treeState.hit)
        {
            ChangeState(treeState.hit);
        }
    }
}
