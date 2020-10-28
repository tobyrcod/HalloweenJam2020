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
        if (playerInput.RawDirInput.x == 0) {
            anim.transform.localScale = new Vector3(0.1f, 0.1f, 1);
        }
        else {
            anim.transform.localScale = new Vector3(playerInput.RawDirInput.x * -1 / 10, anim.transform.localScale.y, anim.transform.localScale.z);
        }

        Move(playerInput.DirInput);
    }

    public void Move(Vector3 input) {
        rb.velocity = new Vector2(input.x, input.y) * player.speed;
        isMoving = input.magnitude >= 0.001f;
    }
}
