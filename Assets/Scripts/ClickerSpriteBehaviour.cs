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
    private int _movementIndex = -1;
    private bool _movementFixed;
    private Vector3 _currentVelocity;

   
    [SerializeField] private SpriteRenderer _renderer;
    private float xExtent, yExtent; // used to keep the sprites from moving outside of the boundaries




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
        _renderer.sprite = _scriptableObject.sprite;
        _movements = _scriptableObject.movementList;

        _flashInterval = _scriptableObject.flahsingInterval;
        _flashDuration = _scriptableObject.flashingDuration;

        xExtent = _renderer.sprite.bounds.extents.x;
        yExtent = _renderer.sprite.bounds.extents.y;
    }

    private void move()
    {
        if (!SpriteClickerManager.instance.isOn)
            return;


        Vector2 newLocation = transform.position + _currentVelocity * Time.deltaTime;

        if (newLocation.x + xExtent > SpriteClickerManager.X_MAX || newLocation.x - xExtent< SpriteClickerManager.X_MIN)
        {
            newLocation.x = Mathf.Clamp(newLocation.x, SpriteClickerManager.X_MIN + xExtent, SpriteClickerManager.X_MAX - xExtent);
            if (!_movements[_movementIndex].doesNotBounce)
                _currentVelocity.x *= -1;
            else
                _currentVelocity.x = 0;
        }

        if (newLocation.y + yExtent > SpriteClickerManager.Y_MAX || newLocation.y - yExtent< SpriteClickerManager.Y_MIN)
        {
            newLocation.y = Mathf.Clamp(newLocation.y, SpriteClickerManager.Y_MIN + yExtent, SpriteClickerManager.Y_MAX - yExtent);
            if (!_movements[_movementIndex].doesNotBounce)
                _currentVelocity.y *= -1;
            else
                _currentVelocity.y = 0;
        }
        transform.position = newLocation;
    }
    /// <summary>
    /// takes care of the flashing mechanism
    /// </summary>
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

    /// <summary>
    /// takes care of the movement setter. 
    /// If the duration of a pattern is set to 0, it will keep that pattern indefinately.
    /// </summary>
    private void movementIterator()
    {
        if (_movementFixed || Time.time < _movementTimer)
            return;

        _movementIndex = (_movementIndex + 1) % _movements.Length;
        _currentVelocity = _movements[_movementIndex].velocity;
        _movementFixed = _movements[_movementIndex].duration == 0; //duration is set to infinite
        _movementTimer = _movements[_movementIndex].duration + Time.time;

    }
}
