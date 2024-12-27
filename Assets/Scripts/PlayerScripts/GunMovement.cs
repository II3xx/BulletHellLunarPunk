using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunMovement : MonoBehaviour
{
    [SerializeField] private GameObject Weapon;
    private readonly float maxDistance = 0.8f;
    private Rigidbody2D rb2D;

    private void Awake()
    {
        rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    float AngleMath(Vector2 Dest)
    {
        return Mathf.Atan2(Dest.y - rb2D.position.y, Dest.x - rb2D.position.x);
    }

    private void Update()
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = AngleMath(currentPos);
        Weapon.transform.localRotation = Quaternion.Euler(0, 0, (angle + 1.5708f) * Mathf.Rad2Deg);
        Weapon.transform.localPosition = new Vector2(maxDistance * Mathf.Cos(angle), maxDistance * Mathf.Sin(angle));
    }
}
