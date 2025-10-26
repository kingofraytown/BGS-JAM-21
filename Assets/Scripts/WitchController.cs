using UnityEngine;
using UnityEngine.InputSystem;

public class WitchController : MonoBehaviour
{
    public Vector2 _movement;
    public float movementSpeed;
    public Animator animator;
    public GameManager gm;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move(_movement);
    }

    public void OnMove(InputAction.CallbackContext cc)
    {

            _movement = cc.ReadValue<Vector2>();
            //Debug.Log(_movement);
    }

    void Move(Vector2 d)
    {
        if(d.x < 0f && d.y > 0f)
        {
            animator.SetInteger("direction", 1);
        }

        else if (d.x == 0f && d.y == 1f)
        {
            animator.SetInteger("direction", 2);
        }

        else if (d.x > 0f && d.y > 0f)
        {
            animator.SetInteger("direction", 3);
        }
        else if (d.x == -1f && d.y == 0f)
        {
            animator.SetInteger("direction", 4);
        }
        else if (d.x == 1f && d.y == 0f)
        {
            animator.SetInteger("direction", 6);
        }
        else if (d.x < 0f && d.y < 0f)
        {
            animator.SetInteger("direction", 7);
        }
        else if (d.x == 0f && d.y == -1f)
        {
            animator.SetInteger("direction", 8);
        }
        else if (d.x > 0f && d.y < 0f)
        {
            animator.SetInteger("direction", 9);
        }
        else
        {
            animator.SetInteger("direction", 5);
        }

        Vector3 curPos = transform.position;
        transform.position = new Vector3(curPos.x + (d.x * Time.deltaTime * movementSpeed), curPos.y + (d.y * Time.deltaTime * movementSpeed), curPos.z);

    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log("colling with " + other.gameObject.name);
        switch (other.tag)
        {
            case "crystal":
                gm.AddCrystal();
                other.GetComponent<CrystalController>().CrystalHit();
                break;
            case "enemy":
                gm.LoseCrystals();
                animator.SetTrigger("hurt");
                break;
            case "goal":
                gm.ShowWinPanel();
                break;
        }
    }
}
