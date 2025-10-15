using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    public float speed = 5f;
    private Vector2 movement, screenBound;
    private float playerHalfWidth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        playerHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        print(playerHalfWidth);
    }

    // Update is called once per frame
    void Update()
    {
        float clampdX = Mathf.Clamp(transform.position.x, -screenBound.x + playerHalfWidth, screenBound.x - playerHalfWidth);
        float clampdY = Mathf.Clamp(transform.position.y, -screenBound.y + playerHalfWidth, screenBound.y - playerHalfWidth);
        Vector2 pos = transform.position;
        pos.x = clampdX;
        pos.y = clampdY;
        transform.position = pos;
    }
}
