using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour
{
    public FighterBehaviour Player;
    public Text UIHealthText;
    public Text UIMeterText;

    private void Start()
    {
        UIHealthText.text = Player.CurrentHealth.ToString();
        UIMeterText.text = Player.MeterAmount.ToString();
    }
                         
    public void ChangeHealthValue()
    {
        UIHealthText.text = Player.CurrentHealth.ToString();
    }

    public void ChangeMeterValue()
    {
        UIMeterText.text = Player.MeterAmount.ToString();
    }
}
