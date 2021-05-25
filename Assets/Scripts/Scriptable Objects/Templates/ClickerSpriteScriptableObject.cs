using UnityEngine;
using System;

[Serializable]
public class Movement
{
    public Vector2 velocity;
    public float duration; //lasts indefinately if set to 0
    public bool doesNotBounce;
}
[CreateAssetMenu(fileName = "new Sprite", menuName = "Sprite Clicker SO")]
public class ClickerSpriteScriptableObject : ScriptableObject
{
    public Movement[] movementList;
    public Sprite sprite;
    public float flahsingInterval;
    public float flashingDuration;

}
