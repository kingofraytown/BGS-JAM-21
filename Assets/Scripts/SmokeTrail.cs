using UnityEngine;

public class SmokeTrail : MonoBehaviour
{
    public GameFloat speed;
    public float minLife;
    public float maxLife;
    public float minSpeed;
    public float maxSpeed;
    public ParticleSystem smoke;
    public float smokeRatio;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        ParticleSystem.MainModule main = smoke.main;
        smokeRatio = Mathf.InverseLerp(minSpeed, maxSpeed, speed.value());
        main.startLifetime = minLife + (maxLife * smokeRatio);
        //main.startSize = maxSize * dustRatio;
    }
}
