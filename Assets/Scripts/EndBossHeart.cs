using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndBossHeart : MonoBehaviour
{
    public float speed = 100;

    void Update()
    {
        transform.Rotate(0, speed * Time.deltaTime, 0);
    }
}
