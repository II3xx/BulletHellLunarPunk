using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody2D))]

public class EnemyMovement : MonoBehaviour
{
    private GameObject player;
    [SerializeField] [Range(0,8)] float minDistance = 0;
    [SerializeField] [Range(0, 10)] float movementSpeed = 2;
    readonly float deadZone = 0.1f;
    Rigidbody2D rb2D;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    float AngleMath(Vector2 Dest)
    {
        return Mathf.Atan2(rb2D.position.y - Dest.y, rb2D.position.x - Dest.x);
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 Dest = player.GetComponent<Rigidbody2D>().position;
        float angle = AngleMath(Dest);
        float length = Mathf.Abs(Vector2.Distance(Dest, rb2D.position));

        if (length + deadZone < minDistance)
        {
            rb2D.velocity = new Vector2(movementSpeed * Mathf.Cos(angle), movementSpeed * Mathf.Sin(angle));
        }
        else if (length - deadZone > minDistance)
        {
            rb2D.velocity = new Vector2(-movementSpeed * Mathf.Cos(angle), -movementSpeed * Mathf.Sin(angle));
        }
        else
        {
            rb2D.velocity = new Vector2(0, 0);
            return;
        }
    }
}
