using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SpriteClickerManager : MonoBehaviour
{
    public static SpriteClickerManager instance;
    //area of sprite movement
    public const float X_MAX = 3f;
    public const float X_MIN = -3f;
    public const float Y_MAX = 3f;
    public const float Y_MIN = -3f;

    public GameObject spriteClickerPrefab;
    public ClickerSpriteScriptableObject[] scriptableObjectsList;
    public bool isOn;

    private int score = 0;
    [SerializeField] private Text scoreDisplay;
    // Start is called before the first frame update
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        CheckClick();
    }

    private void CheckClick()
    {
        if (!isOn)
            return;


        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
            if(hit)
            {
                ClickerSpriteBehaviour clickerSpriteBehaviour = hit.collider.GetComponent<ClickerSpriteBehaviour>();
                if(clickerSpriteBehaviour != null && clickerSpriteBehaviour.isFlashing)
                {
                    score++;
                    scoreDisplay.text = score.ToString();
                }
            }
        }
    }
}
