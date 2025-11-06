using UnityEngine;

public class ShadowFollow : MonoBehaviour
{
    public Transform player;
    public float minScale;
    public float maxScale;
    public float maxOpacity;
    public float playerFloor;
    public float playerCeiling;
    public float opacity;
    public SpriteRenderer sr;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        gameObject.transform.position = new Vector3(player.position.x, -1f, 4f);

        float height = Mathf.InverseLerp(playerCeiling, playerFloor, player.position.y);
        float heightInverse = Mathf.InverseLerp(playerFloor, playerCeiling, player.position.y);
        opacity = (maxOpacity/255f) * height;
        sr.color = new Color(1f, 1f, 1f, height);
        float newScale = minScale + heightInverse * maxScale;
        transform.localScale = new Vector3(newScale, newScale, newScale);
    }

    public void ChangeAngle(int a)
    {
        //Quaternion rot = gameObject.transform.rotation;
        switch (a)
        {
            case -1:
                gameObject.transform.rotation = Quaternion.Euler(90, 0, 25);
                break;
            case 0:
                gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                break;
            case 1:
                gameObject.transform.rotation = Quaternion.Euler(90f, 0f, -25f);
                break;
            default:
                gameObject.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
                break;
        }
    }
}
