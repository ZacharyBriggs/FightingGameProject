using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyuBehaviour : FighterBehaviour
{
    public InputScriptable SpAttack1;
    public InputScriptable SpAttack2;
    public InputScriptable SpAttack3;
    public Transform HadoukenSpawnPos;
    public GameObject HadoukenPrefab;
	// Use this for initialization
	void Start ()
    {
        StartUp();
	}
	
	// Update is called once per frame
	void Update ()
    {
        RotationCheck();
        DoubleTapCheck();
        CheckButtons();
	}


    public void DoSpecialOne()
    {
        Instantiate(HadoukenPrefab, HadoukenSpawnPos);
    }

    public override void CheckInput()
    {
        if (InputList.Count > 4)
        {
            int c = 0;
            for (int i = SpAttack3.MoveInput.Count; i > 0; i--)
            {
                if (InputList[InputList.Count - i] != SpAttack3.MoveInput[c])
                    break;
                else
                    c++;

                if (c == SpAttack3.MoveInput.Count)
                {
                    _animator.SetInteger("State", 17);
                    InputList.Add("Sp3");
                    Debug.Log("Sp3");
                    InputList = new List<string> { "None" };
                }
            }
            
            int b = 0;
            for (int i = SpAttack2.MoveInput.Count; i > 0; i--)
            {
                if (InputList[InputList.Count - i] != SpAttack2.MoveInput[b])
                    break;
                else
                    b++;

                if (b == 4)
                {
                    _animator.SetInteger("State", 16);
                    InputList.Add("Sp2");
                    Debug.Log("Sp2");
                    InputList = new List<string> { "None" };
                }
            }

            int a = 0;
            for (int i = SpAttack1.MoveInput.Count; i > 0; i--)
            {
                if (InputList[InputList.Count - i] != SpAttack1.MoveInput[a])
                    break;
                else
                    a++;

                if (a == 4)
                {
                    _animator.SetInteger("State", 15);
                    InputList.Add("Sp1");
                    Debug.Log("Sp1");
                    InputList = new List<string> { "None" };
                }
            }

        }
    }
}
