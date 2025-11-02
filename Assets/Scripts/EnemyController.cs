using UnityEngine;

public class EnemyController : MonoBehaviour
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

    public enum ghostState
    {
        idle = 0,
        awake,
        attack,
        hit,
        dead
    }

    public ghostState currentState = ghostState.idle;

    public void ChangeState(ghostState g)
    {
        switch (g)
        {
            case ghostState.awake:
                animator.SetInteger("state", 1);
                break;
            case ghostState.attack:
                animator.SetInteger("state", 2);
                break;
            case ghostState.hit:
                animator.SetInteger("state", 3);
                break;
        }

        currentState = g;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 adjPos = new Vector3(player.position.x, player.position.y, player.position.z - playerOffset);
        distance = Mathf.Abs(Vector3.Distance(this.transform.position,adjPos));

        if(currentState == ghostState.idle)
        {
            if(distance <= alertRange)
            {
                ChangeState(ghostState.awake);
            }
        }

        if(distance <= attackRange)
        {
            if(chase == false)
            {
                ChangeState(ghostState.attack);
                chase = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, adjPos, ((gSpeed.value() + speed) * Time.deltaTime));
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, -100f),((gSpeed.value() + speed) * Time.deltaTime));
        }

        if(distance < 1)
        {
            gameObject.SetActive(false);
        }
    }

    public void Hit()
    {
        if (currentState != ghostState.hit)
        {
            ChangeState(ghostState.hit);
        }
    }
}
