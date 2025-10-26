using UnityEngine;

public class LandChunkController : MonoBehaviour
{
    public float landSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 cur = transform.position;
        transform.position = new Vector3(cur.x, cur.y, cur.z + (landSpeed * Time.deltaTime));
    }
}
