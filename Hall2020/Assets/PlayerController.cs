using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class PlayerController : Character
{ 
    [SerializeField] float digTime = 1f;
    private float digSpeed;

    [HideInInspector] public bool isDigging = false;
   
    [Space]

    [SerializeField] SliderController sliderDigProgress;
    private float digProgess;
    private Transform diggingCanvas;

    [Space]

    [SerializeField] Transform weaponPrefabParent;
    [SerializeField] GameObject shovelMeleePrefab;
    [SerializeField] float weaponDistanceFromPlayer;

    [Space]

    private PlayerInput playerInput;
    private PlayerMovement playerMovement;

    private Inventory inventory;
    [SerializeField] private InventoryUI inventoryUI;
    [SerializeField] List<Item> startingItems;


    [SerializeField] protected float attackSpeed = 3f;
    [HideInInspector] public bool isAttacking = false;

    //QUANTUMCOOKIE
    public AudioClip gameMusic;
    //QUANTUMCOOKIE
    
    protected override void Awake() {

        base.Awake();

        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        playerInput.OnDigKeyPressEvent += StartDigging;
        playerInput.OnDigKeyReleasedEvent += StopDigging;
        playerInput.OnDigKeyHeldEvent += Digging;

        playerInput.OnPrimaryKeyPressEvent += OnPrimaryKeyPress;

        diggingCanvas = sliderDigProgress.transform.parent;

        inventory = new Inventory(10);
        inventoryUI.Init(inventory);

        for (int i = 0; i < startingItems.Count; i++) {
            inventory.AddItem(startingItems[i]);
        }
        
        AudioManager.Instance.ChangeMusic(gameMusic);
    }

    private void Digging() {

        if (isDigging) {
            if (playerMovement.isMoving) {
                StopDigging();
            }
            else {
                digProgess += digSpeed * Time.deltaTime;
                digProgess = Mathf.Min(1f, digProgess);

                sliderDigProgress.SetValue(digProgess);

                if (digProgess == 1f) {
                    //Create Items to Pickup
                    Item[] itemsToAdd = GameUtils.instance.GetRandomItems(1);
                    //inventory.AddItem
                    for (int i = 0; i < itemsToAdd.Length; i++) {
                        inventory.AddItem(itemsToAdd[i]);
                    }

                    //Stop Digging
                    StopDigging();
                }              
            }
        }
    }

    private void StopDigging() {
        isDigging = false;
        diggingCanvas.gameObject.SetActive(false);
    }

    private void StartDigging() {
        if (!playerMovement.isMoving && !isAttacking && !isDigging) {
            isDigging = true;

            digProgess = 0f;
            sliderDigProgress.SetValue(digProgess);
            digSpeed = 1 / digTime;
            diggingCanvas.gameObject.SetActive(true);
        }
    }

    private void OnPrimaryKeyPress() {
        if (!isDigging && !isAttacking) {
            //Attack

            isAttacking = true;

            Vector2 playerPos = this.transform.position;
            Vector2 mousePos = playerInput.MousePos;
            Vector2 displacement = mousePos - playerPos;
            float modDisplacement = Mathf.Sqrt(Mathf.Pow(displacement.x, 2) + Mathf.Pow(displacement.y, 2));
            Vector2 spawnPosition = playerPos + weaponDistanceFromPlayer * new Vector2(displacement.x / modDisplacement, displacement.y / modDisplacement);

            float theta = AngleBetweenVector2(playerPos, mousePos);

            ShovelMeleeController smc = Instantiate(shovelMeleePrefab, spawnPosition, Quaternion.Euler(0,0, theta), weaponPrefabParent).GetComponent<ShovelMeleeController>();
            smc.Init(this);
            smc.SetShovelSpeed(attackSpeed);
        }
    }
    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2) {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

    protected override void Die() {
        Debug.Log("Die");
    }
}
