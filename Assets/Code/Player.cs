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

    private bool _wasOnGround;
    private bool _onGround;

    private int _runAnimationIndex;
    private float _runAnimationCycle;

    void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();

        _startingPosition = gameObject.transform.position;
        _currentPosition = _startingPosition;
        _velocity = new Vector3(0, 0, 0);

        _wasOnGround = true;
        _onGround = true;

        _runAnimationIndex = 0;
        _runAnimationCycle = 0;
    }

    void Update()
    {
        _wasOnGround = _onGround;
        processGravity();
        processJumping();
        applyVelocity();
        gameObject.transform.position = _currentPosition;

        doAnimations();
    }

    private void processJumping()
    {
        bool tryingToJump = Input.GetKeyDown(KeyCode.Space);
        if (_onGround && tryingToJump)
        {
            Debug.Log("Jumping!");
            _velocity.y += 2.0f;
            _onGround = false;
        }
    }

    private void processGravity()
    {
        _velocity.y -= Time.deltaTime * 5;
        if (_currentPosition.y < _startingPosition.y)
        {
            _onGround = true;
            _velocity.y = 0;
            _currentPosition.y = _startingPosition.y;
        }
    }

    private void applyVelocity()
    {
        _currentPosition += _velocity * Time.deltaTime;
    }

    private void doAnimations()
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
