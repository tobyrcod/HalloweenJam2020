using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;

    private PlayerInput playerInput;
    private PlayerController player;

    public bool isMoving = false;
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        player = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("Horizontal", playerInput.DirInput.x);
        anim.SetFloat("Vertical", playerInput.DirInput.y);
        anim.SetFloat("Magnitude", playerInput.DirInput.magnitude);

        Move(playerInput.DirInput);
    }

    private void Move(Vector3 input) {
        rb.velocity = new Vector2(input.x, input.y) * player.speed;
        isMoving = input.magnitude >= 0.001f;
    }
}
