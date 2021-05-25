using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factories
{
    public abstract GameObject create(params string[] args);
}


public class clickerSpriteFactory : Factories
{
    private Dictionary<string, ClickerSpriteScriptableObject> creationList;

    public clickerSpriteFactory()
    {
        creationList = new Dictionary<string, ClickerSpriteScriptableObject>();
        //TODO: Find a way to automatically populate the dictionary within this class.
    }
    public void AddToList(string name, ClickerSpriteScriptableObject so)
    {
        creationList.Add(name, so);
    }

    /// <summary>
    /// arg1: (string)clickerSrptie type
    /// arg2: (float)x 
    /// arg2: (float)y
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public override GameObject create(params string[] args)
    {
        if (!creationList.ContainsKey(args[0]))
        {// return if the given type does not exist.
            Debug.LogError("given type was not found");
            return null;
        }
        
        ClickerSpriteScriptableObject so = creationList[args[0]];


        Vector2 newLocation;
        if (args.Length == 1)
        {// if location info was not given, spawn at 0,0
            newLocation = Vector3.zero;
        }
        else
        {
            newLocation = new Vector2(float.Parse(args[1]), float.Parse((args[2])));
        }
        GameObject newGO = MonoBehaviour.Instantiate(SpriteClickerManager.instance.spriteClickerPrefab, newLocation, Quaternion.identity);
  
        newGO.GetComponent<ClickerSpriteBehaviour>().init(so);

        /*
        switch (args[0])
        {
            case "circle":
                //so = new circleClickerSprite().clickerScriptableObject;
                break;
            case "square":
                newGO.GetComponent<ClickerSpriteBehaviour>().init(SpriteClickerManager.instance.scriptableObjectsList[1]);
                break;
            case "star":
                newGO.GetComponent<ClickerSpriteBehaviour>().init(SpriteClickerManager.instance.scriptableObjectsList[2]);
                break;
            default:
                GameObject.Destroy(newGO);
                return null;
                break;
        }
        */

        return newGO;
    }
}

//I didn't understand how it can be implemented :(
public abstract class clickerSprite
{
    public abstract ClickerSpriteScriptableObject clickerScriptableObject{ get; }
}
public class circleClickerSprite: clickerSprite
{
    public override ClickerSpriteScriptableObject clickerScriptableObject => SpriteClickerManager.instance.scriptableObjectsList[0];
}

