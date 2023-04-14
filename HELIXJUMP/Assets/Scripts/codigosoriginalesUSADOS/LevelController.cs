using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
    [SerializeField] float speed = 5f;

    void Update()
    {
        Vector3 rot = new Vector3(0f, Input.GetAxis("Horizontal") * speed * Time.deltaTime, 0f);
        transform.Rotate(rot);
    }
}
