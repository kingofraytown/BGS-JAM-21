using UnityEngine;

public class CrystalController : MonoBehaviour
{

    public bool hit = false;
    public Animator animator;
    public float deathTime = 1f;
    public float deathTimer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(hit == true)
        {
            deathTimer -= Time.deltaTime;
            if(deathTimer <= 0)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public void CrystalHit()
    {
        deathTimer = deathTime;
        hit = true;
        animator.SetBool("collect", true);
    }
}
