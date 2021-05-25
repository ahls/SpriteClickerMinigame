using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalManager : MonoBehaviour
{
    public static GlobalManager instance;
    public clickerSpriteFactory clickerSpriteFactory;
    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            clickerSpriteFactory = new clickerSpriteFactory();
        }
    }


}
