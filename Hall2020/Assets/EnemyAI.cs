using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
using UnityEditorInternal;

public class EnemyAI : Character
{
    public Transform target;

    public float nextWaypointDistance = 3f;

    Path path;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;

    Seeker seeker;
    Rigidbody2D rb;

    [SerializeField] Transform enemyGFX;

    private void Start() {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();

        InvokeRepeating("UpdatePath", 0f, 0.1f);
    }

    private void UpdatePath() {
        if (seeker.IsDone()) {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }

    void OnPathComplete(Path p) {
        if (!p.error) {
            path = p;
            currentWaypoint = 0;
        }
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
            enemyGFX.localScale = new Vector3(-1f, 2f, 1f);
        }
        else if (force.x <= -0.01) {
            enemyGFX.localScale = new Vector3(1f, 2f, 1f);
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
