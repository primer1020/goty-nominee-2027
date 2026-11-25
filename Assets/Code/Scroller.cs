using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private float scrollSpeed;
    private Camera activeCamera;
    private Vector3 objectSize;
    private Vector3 originalPosition;
    private GameObject rightClone;
    private Vector3 rightClonePosition;

    public Bounds GetBounds()
    {
        var renderers = GetComponentsInChildren<SpriteRenderer>();
        if (renderers.Length == 0)
            return new Bounds(transform.position, Vector3.zero);

        Bounds combined = renderers[0].bounds;

        for (int i = 1; i < renderers.Length; i++)
            combined.Encapsulate(renderers[i].bounds);

        return combined;
    }

    void Start()
    {
        scrollSpeed = Variables.Object(gameObject).Get<float>("ScrollSpeed");

        activeCamera = Camera.main;

        objectSize = GetBounds().size;

        originalPosition = gameObject.transform.position;

        rightClone = Instantiate(gameObject.transform.GetChild(0).gameObject);
        rightClonePosition = originalPosition;
        rightClonePosition.x = originalPosition.x + objectSize.x;
        rightClone.transform.position = rightClonePosition;
    }

    void Update()
    {
        originalPosition.x -= Time.deltaTime * scrollSpeed;
        rightClonePosition.x -= Time.deltaTime * scrollSpeed;

        if (originalPosition.x + objectSize.x < 0)
            originalPosition.x += objectSize.x * 2;

        if (rightClonePosition.x + objectSize.x < 0)
            rightClonePosition.x += objectSize.x * 2;

        gameObject.transform.position = originalPosition;
        rightClone.transform.position = rightClonePosition;
    }
}
