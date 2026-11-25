using Unity.VisualScripting;
using UnityEngine;

public class Scroller : MonoBehaviour
{
    private GameObject _rightClone;

    private Vector3 _rightClonePosition;
    private Vector3 _originalPosition;
    private Vector3 _objectSize;

    [SerializeField] private float _scrollSpeed;

    void Start()
    {
        initializeVariables();
        createClone();
    }

    void Update()
    {
        _originalPosition.x -= Time.deltaTime * _scrollSpeed;
        _rightClonePosition.x -= Time.deltaTime * _scrollSpeed;

        if (_originalPosition.x + _objectSize.x < 0)
            _originalPosition.x += _objectSize.x * 2;

        if (_rightClonePosition.x + _objectSize.x < 0)
            _rightClonePosition.x += _objectSize.x * 2;

        gameObject.transform.position = _originalPosition;
        _rightClone.transform.position = _rightClonePosition;
    }

    private void initializeVariables()
    {
        _objectSize = getGameObjectChildrenBounds().size;

        _originalPosition = gameObject.transform.position;
    }

    private void createClone()
    {
        _rightClone = Instantiate(gameObject.transform.GetChild(0).gameObject);
        _rightClonePosition = _originalPosition;
        _rightClonePosition.x += _objectSize.x;
        _rightClone.transform.position = _rightClonePosition;
    }

    private Bounds getGameObjectChildrenBounds()
    {
        var renderers = GetComponentsInChildren<SpriteRenderer>();
        if (renderers.Length == 0)
            return new Bounds(transform.position, Vector3.zero);
        Bounds combined = renderers[0].bounds;

        for (int i = 1; i < renderers.Length; i++)
            combined.Encapsulate(renderers[i].bounds);

        return combined;
    }
}
