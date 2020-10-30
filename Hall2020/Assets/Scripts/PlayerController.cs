using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.UI;
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
    private GraveManager graveManager;

//    private Inventory inventory;
//    [SerializeField] private InventoryUI inventoryUI;
//    [SerializeField] List<Item> startingItems;

    [SerializeField] protected float attackSpeed = 3f;
    [HideInInspector] public bool isAttacking = false;

    //QUANTUMCOOKIE
    public AudioClip gameMusic;
    //QUANTUMCOOKIE

    private bool withinGraveRange = false;
    private Grave currentGrave = null;

    public Image inventoryDisplay;
    public Sprite blankSprite;

    private bool nearMonster = false;
    private bool isFixing = false;

    public GameObject head, torso, arm1, arm2, hip, leg1, leg2;
    
    protected override void Awake() {

        base.Awake();

        playerInput = GetComponent<PlayerInput>();
        playerMovement = GetComponent<PlayerMovement>();

        playerInput.OnDigKeyPressEvent += StartDigging;
        playerInput.OnDigKeyReleasedEvent += StopDigging;
        playerInput.OnDigKeyHeldEvent += Digging;
        
        playerInput.OnDigKeyPressEvent += StartFixing;
        playerInput.OnDigKeyReleasedEvent += StopFixing;
        playerInput.OnDigKeyHeldEvent += Fixing;

        playerInput.OnPrimaryKeyPressEvent += OnPrimaryKeyPress;

        diggingCanvas = sliderDigProgress.transform.parent;

        graveManager = GameManager.Instance.GetComponent<GraveManager>();
        
//        inventory = new Inventory(10);
//        inventoryUI.Init(inventory);
//
//        for (int i = 0; i < startingItems.Count; i++) {
//            inventory.AddItem(startingItems[i]);
//        }

        inventoryDisplay.sprite = blankSprite;
        
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
//                    //Create Items to Pickup
//                    Item[] itemsToAdd = GameUtils.instance.GetRandomItems(1);
//                    //inventory.AddItem
//                    for (int i = 0; i < itemsToAdd.Length; i++) {
//                        inventory.AddItem(itemsToAdd[i]);
//                    }

                    Item item = graveManager.CollectRandomItem(currentGrave.owner);

                    if (item == null)
                    {
                        //POPUP: "This body is useless"
                    }
                    else
                    {
                        Debug.Log(item);
                        graveManager.Collected(currentGrave.owner, item);
                        inventoryDisplay.sprite = item.icon;
                    }
                    
                    Debug.Log("Dug " + currentGrave.owner);
                    currentGrave.fresh = false;
                    
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
        if (!playerMovement.isMoving && !isAttacking && !isDigging && currentGrave != null && !graveManager.HasItem())
        {
            if (!currentGrave.fresh) return;
            
            isDigging = true;

            digProgess = 0f;
            sliderDigProgress.SetValue(digProgess);
            digSpeed = 1 / digTime;
            diggingCanvas.gameObject.SetActive(true);
        }
    }

    private void StartFixing()
    {
        if (nearMonster && !playerMovement.isMoving && !isAttacking && !isFixing && graveManager.HasItem())
        {
            isFixing = true;

            digProgess = 0f;
            sliderDigProgress.SetValue(digProgess);
            digSpeed = 1 / digTime;
            diggingCanvas.gameObject.SetActive(true);
        }
    }

    private void Fixing()
    {
        if (isFixing && graveManager.HasItem()) {
            if (playerMovement.isMoving) {
                StopFixing();
            }
            else {
                digProgess += digSpeed * Time.deltaTime;
                digProgess = Mathf.Min(1f, digProgess);

                sliderDigProgress.SetValue(digProgess);

                if (digProgess == 1f) {
                    
                    //TODO
                    Debug.Log("Fixed part");


                    switch (graveManager.CurrentItem().name)
                    {
                        case "Head": head.SetActive(true);
                            break;
                        case "Torso": torso.SetActive(true);
                            break;
                        case "Arm1": arm1.SetActive(true);
                            break;
                        case "Arm2": arm2.SetActive(true);
                            break;
                        case "Hip": hip.SetActive(true);
                            break;
                        case "Leg1": leg1.SetActive(true);
                            break;
                        case "Leg2": leg2.SetActive(true);
                            break;
                    }

                    graveManager.FixedItem();
                    inventoryDisplay.sprite = blankSprite;
                    
                    //Stop Fixing
                    StopFixing();
                }              
            }
        }
    }

    private void StopFixing()
    {
        isFixing = false;
        diggingCanvas.gameObject.SetActive(false);
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Grave>() != null)
        {
            currentGrave = other.GetComponent<Grave>();
        }
        else if (other.CompareTag("Home"))
        {
            nearMonster = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Grave>() != null)
        {
            currentGrave = null;
        }
        else if (other.CompareTag("Home"))
        {
            nearMonster = false;
        }
    }
}
