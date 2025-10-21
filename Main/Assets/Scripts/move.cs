using UnityEngine;

public class move : MonoBehaviour
{
    public int speed;
    private Rigidbody2D rb;
    private int hmoveInput, vmoveInput;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = new Vector2(hmoveInput * speed, vmoveInput * speed);
    }

    public void PointerDownLeft()
    {
        hmoveInput = -1;
    }

    public void PointerDownRight()
    {
        hmoveInput = 1;
    }

    public void PointerDownUp()
    {
        vmoveInput = 1;
    }

    public void PointerDownDown()
    {
        vmoveInput = -1;
    }

    public void StopMoveV()
    {
        vmoveInput = 0;
    }

    public void StopMoveH()
    {
        hmoveInput = 0;
    }
}
