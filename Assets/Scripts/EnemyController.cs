using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed;
    public Transform player;
    public float attackRange;
    public float playerOffset = 10f;
    public float distance;
    public Animator animator;
    public bool chase = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Vector3 adjPos = new Vector3(player.position.x, player.position.y, player.position.z - playerOffset);
        distance = Vector3.Distance(this.transform.position,adjPos);

        if(distance <= attackRange)
        {
            if(chase == false)
            {
                animator.SetBool("chase", true);
                chase = true;
            }
            transform.position = Vector3.MoveTowards(transform.position, adjPos, -1* speed * Time.deltaTime);
        } else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y, 100f),(speed * Time.deltaTime));
        }

        if(distance < 1)
        {
            gameObject.SetActive(false);
        }
    }
}
