using UnityEngine;

public class DustTrail : MonoBehaviour
{
    public Transform player;
    public ParticleSystem dust;
    public float yLimit;
    public float playerFloor;
    public float maxSize;
    public float maxRate;
    public float dustRatio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LateUpdate()
    {
        gameObject.transform.position = new Vector3(player.position.x, -1f, 4f);
        ParticleSystem.EmissionModule em = dust.emission;
        ParticleSystem.MainModule main = dust.main;
        if(player.position.y <= yLimit)
        {
            float yRange = yLimit - playerFloor;
            dustRatio = Mathf.InverseLerp(yLimit, playerFloor, player.position.y);
            em.rateOverTime = maxRate * dustRatio;
            main.startSize = maxSize * dustRatio;

        }
        else
        {
            em.rateOverTime = 0;
        }

    }
}
