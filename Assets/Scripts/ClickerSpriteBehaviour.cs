using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickerSpriteBehaviour : MonoBehaviour
{
    private ClickerSpriteScriptableObject _scriptableObject;

    private float _flashTimer = 0;
    private float _flashInterval;
    private float _flashDuration;
    public bool isFlashing = true;

    private Movement[] _movements;
    private float _movementTimer;
    private int _movementIndex = 0;
    private bool _movementFixed;
    private Vector3 _currentVelocity;

    [SerializeField] private SpriteRenderer _renderer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!SpriteClickerManager.instance.isOn)
            return;
        flashIterator();
        movementIterator();
    }

    private void FixedUpdate()
    {
        move();
    }
    public void init(ClickerSpriteScriptableObject scriptableObject)
    {
        _scriptableObject = scriptableObject;
        _movements = _scriptableObject.movementList;

        _flashInterval = _scriptableObject.flahsingInterval;
        _flashDuration = _scriptableObject.flashingDuration;
    }

    private void move()
    {
        transform.position += _currentVelocity;
    }

    private void flashIterator()
    {
        if (Time.time < _flashTimer)
            return;

        isFlashing = !isFlashing;
        if(isFlashing)
        {
            _flashTimer = Time.time + _flashDuration;
            _renderer.color = Color.red;
        }
        else
        {
            _flashTimer = Time.time + _flashInterval;
            _renderer.color = Color.white;
        }
    }
    private void movementIterator()
    {
        if (_movementFixed || Time.time < _movementTimer)
            return;

        _currentVelocity = _movements[_movementIndex].velocity;
        _movementFixed = _movements[_movementIndex].duration == 0; //duration is set to infinite
        _movementTimer = _movements[_movementIndex].duration + Time.time;

        _movementIndex = (_movementIndex + 1) % _movements.Length;
    }
}
