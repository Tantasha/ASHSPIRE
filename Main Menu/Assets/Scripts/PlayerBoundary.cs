using UnityEngine;

public class PlayerBoundary : MonoBehaviour
{
    public float speed = 5f;//speed of player movement
    private Vector2 movement;//store player direction
    private Vector2 screenBound;//convert screen space to world space
    private float playerHalfWidth;//for player not to move halfway off screen

    void Start()
    {
        //convert screen size to world space coordinates
        screenBound = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        //ensure boundary clamping, keeps player on screen
        playerHalfWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
        //for debugging
        print(playerHalfWidth);
    }

    // Update is called once per frame
    void Update()
    {
        // Clamp the player's X position so it stays within the horizontal screen bounds.
        //(+/- playerHalfWidth) prevents player from going off screen
        float clampdX = Mathf.Clamp(transform.position.x, -screenBound.x + playerHalfWidth, screenBound.x - playerHalfWidth);
        // Clamp the player's Y position so it stays within the vertical screen bounds.
        float clampdY = Mathf.Clamp(transform.position.y, -screenBound.y + playerHalfWidth, screenBound.y - playerHalfWidth);
        // Create a new position vector with the clamped X and Y values.
        Vector2 pos = transform.position;
        pos.x = clampdX;
        pos.y = clampdY;
        transform.position = pos;// Apply new position back to player transforms
    }
}
