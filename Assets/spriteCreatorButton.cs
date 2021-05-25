using UnityEngine;
using UnityEngine.UI;
public class spriteCreatorButton : MonoBehaviour
{
    [SerializeField] private string spriteType;
    [SerializeField] InputField X, Y;

    public void onClick()
    {
        string x = X.text;
        string y = Y.text;
        if (x == "" || y == "")
        {
            string[] args = { spriteType};
            GlobalManager.instance.clickerSpriteFactory.create(args);
        }
        else
        {
            string[] args = { spriteType, X.text, Y.text };
            GlobalManager.instance.clickerSpriteFactory.create(args);
        }
    }
}
