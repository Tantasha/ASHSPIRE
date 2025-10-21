using UnityEngine;

public class move : MonoBehaviour
{
<<<<<<< HEAD
    public int speed;
    private Rigidbody2D rb;
    private int hmoveInput, vmoveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
=======
    public int speed;//speed of movement
    private Rigidbody2D rb;//reference to the Rigidbody2D component
    private int hmoveInput;//moves horizontally
    private int vmoveInput;//moves vertically

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();//allows player to control the physics movement later
>>>>>>> AccountLink
    }

    // Update is called once per frame
    void Update()
    {
<<<<<<< HEAD
=======
        //set Rigidbody2D velocity each frame based on player input
>>>>>>> AccountLink
        rb.linearVelocity = new Vector2(hmoveInput * speed, vmoveInput * speed);
    }

    public void PointerDownLeft()
    {
<<<<<<< HEAD
        hmoveInput = -1;
=======
        hmoveInput = -1;//moves left once UI button is pressed
>>>>>>> AccountLink
    }

    public void PointerDownRight()
    {
<<<<<<< HEAD
        hmoveInput = 1;
=======
        hmoveInput = 1;//move right once UI button is pressed
>>>>>>> AccountLink
    }

    public void PointerDownUp()
    {
<<<<<<< HEAD
        vmoveInput = 1;
=======
        vmoveInput = 1;//moves up once UI button is pressed
>>>>>>> AccountLink
    }

    public void PointerDownDown()
    {
<<<<<<< HEAD
        vmoveInput = -1;
=======
        vmoveInput = -1;//moves down when UI button is pressed
>>>>>>> AccountLink
    }

    public void StopMoveV()
    {
<<<<<<< HEAD
        vmoveInput = 0;
=======
        vmoveInput = 0;//stops moving vertically
>>>>>>> AccountLink
    }

    public void StopMoveH()
    {
<<<<<<< HEAD
        hmoveInput = 0;
=======
        hmoveInput = 0;//stops moving horizontally
>>>>>>> AccountLink
    }
}
