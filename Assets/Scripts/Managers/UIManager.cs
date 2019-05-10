using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Image playerOneImage;
    public Image enemyImage;
    public static UIManager Instance;
    // Start is called before the first frame update
    void Start()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        Destroy(gameObject);

    }
    public void ChangePlayerHealth(PlayerType playerType,float playerHealth) 
    {
        switch (playerType)
        {
            case PlayerType.Player:
                playerOneImage.fillAmount = playerHealth / 100;
                break;
            case PlayerType.Enemy:
                enemyImage.fillAmount = playerHealth / 100;
                break;
            default:
                break;
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
