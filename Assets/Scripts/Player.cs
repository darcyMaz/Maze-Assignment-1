using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    CharacterController cc;
    float move_speed;
    float rotation_speed;
    int health;
    int init_health;
    public Vector3 start_pos;

    Boolean reset_pos_lock;

    void Start()
    {
        // Init the character controller.
        cc = GetComponent<CharacterController>();

        // Arbitrary move speed.
        move_speed = 200f;

        // Arbitrary rotation speed.
        rotation_speed = 0.6f;

        // Set health
        init_health = 5;
        health = init_health;

        reset_pos_lock = false;
    }

    
    void Update()
    {
        // ROTATION TRANSFORMATION
        // For now, rotation will be based on K and L buttons instead of the mouse.
        if (Input.GetKey(KeyCode.K))
        {
            transform.Rotate(new Vector3(0, 1, 0), -rotation_speed);
        }
        if (Input.GetKey(KeyCode.L))
        {
            transform.Rotate(new Vector3(0, 1, 0), rotation_speed);
        }

        // POSITION TRANSFORMATION
        Vector3 move_vector = new Vector3(0, 0, 0); //new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (Input.GetKey(KeyCode.W))
        {
            move_vector += new Vector3(0, 0, 1);
        }
        if (Input.GetKey(KeyCode.S))
        {
            move_vector += new Vector3(0, 0, -1);
        }
        if (Input.GetKey(KeyCode.A))
        {
            move_vector += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            move_vector += new Vector3(1, 0, 0);
        }

        cc.SimpleMove(transform.rotation * move_vector * move_speed * Time.deltaTime);
        

        if (reset_pos_lock)
        {
            transform.position = start_pos; // this does not
            transform.rotation = Quaternion.identity;  // this works
            reset_pos_lock = false;
        }
        
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
    }

    public int GetHealth()
    {
        return health;
    }

    public void ResetPosition()
    {
        reset_pos_lock = true;
    }

    public void ResetPlayer()
    {
        health = init_health;
        ResetPosition();
    }

}
