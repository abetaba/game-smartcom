using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class player : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed;
    Camera cam;
    Transform my;
    Rigidbody2D body;
    Vector3 bullet_pos;
    float next_fire;
    float fire_rate;
    private GameObject projectile;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        my = GetComponent<Transform>();
        body = GetComponent<Rigidbody2D>();
        if (Time.time > next_fire)
        {
            next_fire = Time.time + fire_rate;
            fire();
        }

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = dir * speed;

        float camDis = cam.transform.position.y - my.position.y;
        Vector3 mouse = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, camDis));
        float AngleRad = Mathf.Atan2(mouse.y - my.position.y, mouse.x - my.position.x);
        float angle = (180 / Mathf.PI) * AngleRad - 90;

        body.rotation = angle;
    }
    void fire()
    {
        bullet_pos = transform.position;
        GameObject new_bullet = Instantiate(projectile, bullet_pos, Quaternion.identity);

        Quaternion direction = transform.rotation;
        var radians = direction.z * Mathf.Deg2Rad;

        float x = Mathf.Cos(radians);
        float y = Mathf.Sin(radians);

        new_bullet.GetComponent<bullet_flying>().velX = x;
        new_bullet.GetComponent<bullet_flying>().velY = y;


    }
}

internal class bullet_flying
{
    internal float velY;
    internal float velX;
}