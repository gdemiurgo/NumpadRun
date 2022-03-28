using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour
{
    [SerializeField] private int num;
    [SerializeField] private Collider2D col;
    [SerializeField] private TextMesh[] texts;

    private bool canCollide;

    private void OnEnable()
    {
        SetNum();
        canCollide = true;
    }

    public void SetNum()
    {
        num = Random.Range(1, GameController.instance.MaxNums() + 1);
        foreach(TextMesh text in texts)
        {
            text.text = num.ToString();
        }
    }

    private void Update()
    {
        float xLimit = Camera.main.transform.position.x - 20;

        if (transform.position.x < xLimit)
        {
            gameObject.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        ControlCollider();
    }

    public int PlatformLength()
    {
        return texts.Length + 1;
    }

    private void ControlCollider()
    {
        col.enabled = num == Player.instance.Num && canCollide;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //canCollide = !collision.CompareTag("Player");
    }
}
