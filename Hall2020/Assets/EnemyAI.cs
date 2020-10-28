using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditorInternal;
using System;

public class EnemyAI : Character {
    public Character player;
    private Collider2D playerCollider;
    public Character monster;
    private Collider2D monsterCollider;

    public Character target;

    public float damage = 5f;
    public float engageDistance = 1f;
    public float attackDistance = 1f;
    public float attackCooldown = 1f;
    private float lastAttackTime;

    [Space]

    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    [SerializeField] Transform enemyGFX;
    [SerializeField] Transform alertCanvas;

    private void Start() {
        playerCollider = player.GetComponent<Collider2D>();
        monsterCollider = monster.GetComponent<Collider2D>();

        target = monster;
        lastAttackTime = 0f;

        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    private void UpdatePath() {
        if (seeker.IsDone()) {
            Vector2 targetPosition;
            if (target == monster) {
                targetPosition = monsterCollider.ClosestPoint(rb.position);
            }
            else {
                targetPosition = playerCollider.ClosestPoint(rb.position);
            }
            seeker.StartPath(rb.position, targetPosition, OnPathComplete);
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
    }

    private void Update() {
        float distanceToPlayer = Vector2.Distance(rb.position, player.transform.position);
 
        if (distanceToPlayer <= engageDistance) {
            alertCanvas.gameObject.SetActive(true);
            ChangeTarget(player);
        }
        else {
            alertCanvas.gameObject.SetActive(false);
            ChangeTarget(monster);
        }

        float distanceToTarget;
        if (target == monster) {
            distanceToTarget = Vector2.Distance(monsterCollider.ClosestPoint(rb.position), rb.position);
        }
        else {
            distanceToTarget = Vector2.Distance(playerCollider.ClosestPoint(rb.position), rb.position);
        }

        if (distanceToTarget <= attackDistance) {
            if (lastAttackTime + attackCooldown <= Time.time) {
                Attack();
            }
        }

        Debug.Log(target);
    }

    private void Attack() {
        lastAttackTime = Time.time;
        target.TakeDamage(damage);
    }

    private void ChangeTarget(Character target) {
        this.target = target;
    }

    private void FixedUpdate() {
        if (path == null)
            return;

        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        }
        else {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
        Vector2 force = direction * speed * 100f * Time.deltaTime;

        rb.AddForce(force);

        float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWaypointDistance) {
            currentWaypoint++;
        }

        if (force.x >= 0.01) {
            enemyGFX.localScale = new Vector3(-1f, enemyGFX.localScale.y, enemyGFX.localScale.z);
        }
        else if (force.x <= -0.01) {
            enemyGFX.localScale = new Vector3(1f, enemyGFX.localScale.y, enemyGFX.localScale.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        Debug.Log(collision.gameObject.name);
        if (collision.CompareTag("Weapon")) {
            Weapon weapon = collision.GetComponentInParent<Weapon>();

            //Knockback
            Vector2 direction = (rb.position - (Vector2)weapon.player.transform.position).normalized;
            Vector2 force = direction * weapon.knockback;

            rb.AddForce(force);

            //Damage
            TakeDamage(weapon.damage);
        }
    }
}
