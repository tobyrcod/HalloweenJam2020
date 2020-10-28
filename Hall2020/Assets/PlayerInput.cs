using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] KeyCode KeyPlayerDig;
    public Action OnDigKeyPressEvent;
    public Action OnDigKeyHeldEvent;
    public Action OnDigKeyReleasedEvent;

    [Space]

    [SerializeField] KeyCode KeyPlayerPrimary;
    public Action OnPrimaryKeyPressEvent;

    [SerializeField] Camera cam;
    public Vector2 MousePos;

    [HideInInspector] public Vector3 DirInput { get; private set; }

    // Update is called once per frame
    void Update()
    {
        DirInput = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0.0f);
        MousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetKeyDown(KeyPlayerDig)) {
            OnDigKeyPressEvent?.Invoke();
        }

        if (Input.GetKey(KeyPlayerDig)) {
            OnDigKeyHeldEvent?.Invoke();
        }

        if (Input.GetKeyUp(KeyPlayerDig)) {
            OnDigKeyReleasedEvent?.Invoke();
        }

        if (Input.GetKeyDown(KeyPlayerPrimary)) {
            if (!EventSystem.current.IsPointerOverGameObject()) {
                OnPrimaryKeyPressEvent?.Invoke();
            }
        }
    }
}
