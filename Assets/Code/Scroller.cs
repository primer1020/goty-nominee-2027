using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Vector3 originalPosition;

    private GameObject rightClone;
    private Vector3 rightClonePosition;

    private GameObject leftClone;
    private Vector3 leftClonePosition;

    void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        rightClone = Instantiate(gameObject.transform.GetChild(0).gameObject);
        leftClone = Instantiate(gameObject.transform.GetChild(0).gameObject);

        originalPosition = gameObject.transform.localPosition;

        rightClonePosition = originalPosition;
        rightClonePosition.x = originalPosition.x - spriteRenderer.bounds.size.x;
        rightClone.transform.localPosition = rightClonePosition;

        leftClonePosition = originalPosition;
        leftClonePosition.y = originalPosition.x + spriteRenderer.bounds.size.y;
        leftClone.transform.localPosition = leftClonePosition;
    }

    void Update()
    {
        originalPosition.x -= Time.deltaTime;
        rightClonePosition.x -= Time.deltaTime;
        leftClonePosition.x -= Time.deltaTime;

        gameObject.transform.localPosition = originalPosition;
        rightClone.transform.localPosition = rightClonePosition;
        leftClone.transform.localPosition = leftClonePosition;
    }
}
