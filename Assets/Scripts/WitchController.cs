using UnityEngine;
using UnityEngine.InputSystem;

public class WitchController : MonoBehaviour
{
    public Vector2 _movement;
    public float movementSpeed;
    public Animator animator;
    public GameManager gm;
    public GameObject hitSound;
    public float minY = 0.8f;
    public float maxY = 4f;
    public float minX = -3f;
    public float maxX = 3f;
    public ParticleSystem ps;
    public ParticleSystem treeleaves;
    public ParticleSystem smoke;
    public ShadowFollow shadow;
    Vector3 targetPosition;
    public GameObject crystalDropSFX;
    public bool dead = false;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (dead == false)
        {
            Move(_movement);
        }
    }

    public void OnMove(InputAction.CallbackContext cc)
    {

        _movement = cc.ReadValue<Vector2>();
        Debug.Log(_movement);
        // normalize output
        if (_movement.x > 0.25f)
        {
            _movement.x = 1f;
        } else if(_movement.x < -0.25f)
        {
            _movement.x = -1f;
        } else
        {
            _movement.x = 0f;
        }

        if (_movement.y > 0.25f)
        {
            _movement.y = 1f;
        }
        else if (_movement.y < -0.25f)
        {
            _movement.y = -1f;
        }
        else
        {
            _movement.y = 0f;
        }

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

        shadow.ChangeAngle((int)d.x);
        Vector3 curPos = transform.position;
        float crystalAccel = gm.speed.value() / 10f;
        targetPosition = new Vector3(curPos.x + (d.x * Time.deltaTime * (movementSpeed + crystalAccel)), curPos.y + (d.y * Time.deltaTime * (movementSpeed + crystalAccel)), curPos.z);

        targetPosition.x = Mathf.Clamp(targetPosition.x, minX, maxX);
        targetPosition.y = Mathf.Clamp(targetPosition.y, minY, maxY);

        transform.position = targetPosition;

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
                int burstCount = gm.LoseCrystals();
                ParticleSystem.Burst b = ps.emission.GetBurst(0);
                other.GetComponent<EnemyController>().Hit();
                b.count = burstCount;
                ps.emission.SetBurst(0, b);
                ps.Play();
                animator.SetTrigger("hurt");
                hitSound.SetActive(true);
                hitSound.SetActive(false);
                crystalDropSFX.SetActive(true);
                crystalDropSFX.SetActive(false);
                break;
            case "tree":
                treeleaves.Play();
                animator.SetTrigger("hurt");
                hitSound.SetActive(true);
                hitSound.SetActive(false);
                crystalDropSFX.SetActive(true);
                crystalDropSFX.SetActive(false);
                other.GetComponent<TreeMonster>().Hit();
                int bc = gm.LoseCrystals();
                ParticleSystem.Burst bt = ps.emission.GetBurst(0);
                bt.count = bc;
                ps.emission.SetBurst(0, bt);
                ps.Play();
                break;
            case "goal":
                //gm.speed.SetValue(0);
                gm.ShowWinPanel();
                break;
        }
    }

    public void Die()
    {
        //start death animation
        //stop particle effects
        dead = true;
        ps.Stop();
        smoke.Stop();
        animator.SetInteger("direction", 10);
    }
}
