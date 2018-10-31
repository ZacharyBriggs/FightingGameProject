using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterBehaviour : MonoBehaviour
{
    public Effect SpAttack1;
    public GameObject Projectile;
    private List<string> InputList = new List<string> { "None"};
	// Update is called once per frame
	void Update ()
    {
        if (InputList == SpAttack1.MoveInput)
        {
            SpAttack1.DoEffect(this.transform.position, Projectile);
            InputList = new List<string> { "None"};
        }

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (InputList[InputList.Count-1] != "DownRight")
                InputList.Add("DownRight");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (InputList[InputList.Count-1] != "Down")
                InputList.Add("Down");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (InputList[InputList.Count-1] != "Right")
                InputList.Add("Right");
            foreach(var input in InputList)
            {
                Debug.Log(input);
            }
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            InputList.Add("Attack");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
        }
    }
}
