using UnityEngine;

public class LandChunkController : MonoBehaviour
{
    public float landSpeed;
    public GameFloat globalSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cur = transform.position;
        transform.position = new Vector3(cur.x, cur.y, cur.z + (-1* globalSpeed.value() * Time.deltaTime));

        if(transform.position.z < -80f)
        {
            gameObject.SetActive(false);
        }
    }
}
