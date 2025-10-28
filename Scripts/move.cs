using UnityEngine;

public class move : MonoBehaviour
{
    public int speed;//speed of movement
    private Rigidbody2D rb;//reference to the Rigidbody2D component
    private int hmoveInput;//moves horizontally
    private int vmoveInput;//moves vertically

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//allows player to control the physics movement later
    }

    // Update is called once per frame
    void Update()
    {
        //set Rigidbody2D velocity each frame based on player input
        rb.linearVelocity = new Vector2(hmoveInput * speed, vmoveInput * speed);
    }

    public void PointerDownLeft()
    {
        hmoveInput = -1;//moves left once UI button is pressed
    }

    public void PointerDownRight()
    {
        hmoveInput = 1;//move right once UI button is pressed
    }

    public void PointerDownUp()
    {
        vmoveInput = 1;//moves up once UI button is pressed
    }

    public void PointerDownDown()
    {
        vmoveInput = -1;//moves down when UI button is pressed
    }

    public void StopMoveV()
    {
        vmoveInput = 0;//stops moving vertically
    }

    public void StopMoveH()
    {
        hmoveInput = 0;//stops moving horizontally
    }
}
