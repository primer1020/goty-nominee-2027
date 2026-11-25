using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private Sprite[] _runningSprites;

    private SpriteRenderer _spriteRenderer;

    private Vector3 _startingPosition;
    private Vector3 _currentPosition;
    private Vector3 _velocity;

    private bool _onGround;

    private int _runAnimationIndex;
    private float _runAnimationCycle;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _startingPosition = gameObject.transform.position;
        _currentPosition = _startingPosition;
        _velocity = new Vector3(0, 0, 0);

        _onGround = true;

        _runAnimationIndex = 0;
        _runAnimationCycle = 0;
    }

    void Update()
    {
        ProcessGravity();
        ProcessJumping();
        ApplyVelocity();
        gameObject.transform.position = _currentPosition;

        DoAnimations();
    }

    private void ProcessJumping()
    {
        bool tryingToJump = Input.GetKeyDown(KeyCode.Space);
        if (_onGround && tryingToJump)
        {
            Debug.Log("Jumping!");
            _velocity.y += 2.0f;
            _onGround = false;
        }
    }

    private void ProcessGravity()
    {
        _velocity.y -= Time.deltaTime * 5;
        if (_currentPosition.y < _startingPosition.y)
        {
            _onGround = true;
            _velocity.y = 0;
            _currentPosition.y = _startingPosition.y;
        }
    }

    private void ApplyVelocity()
    {
        _currentPosition += _velocity * Time.deltaTime;
    }

    private void DoAnimations()
    {
        _runAnimationCycle += Time.deltaTime;
        if (_runAnimationCycle > 0.1f) 
        {
            _runAnimationIndex++;
            _runAnimationCycle = 0;
        }

        if (_onGround)
            _spriteRenderer.sprite = _runningSprites[_runAnimationIndex % _runningSprites.Length];
        else
            _spriteRenderer.sprite = _runningSprites[_runningSprites.Length - 1];
    }
}
