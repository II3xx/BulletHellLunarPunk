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

    private void Update()
    {
        Vector3 currentPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = LunarMath.VectorAngle(currentPos, rb2D.position);
        Weapon.transform.localRotation = Quaternion.Euler(0, 0, (angle + 1.5708f) * Mathf.Rad2Deg);
        Weapon.transform.localPosition = new Vector2(maxDistance * Mathf.Cos(angle), maxDistance * Mathf.Sin(angle));
    }
}
