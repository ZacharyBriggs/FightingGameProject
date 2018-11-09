using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RyuBehaviour : FighterBehaviour
{
    public InputScriptable SpAttack1;
    public InputScriptable SpAttack2;
    public InputScriptable SpAttack3;
    public InputScriptable UAttack1;
    public Transform HadoukenSpawnPos;
    public Transform ShinkuuHadoukenSpawnPos;
    public GameObject HadoukenPrefab;
    public GameObject SHadoukenPrefab;
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

    public void DoUltraOne()
    {
        Instantiate(SHadoukenPrefab, ShinkuuHadoukenSpawnPos);
    }
    public override bool CheckInput()
    {
        if (InputList.Count > 4)
        {
            int a = 0;
            int b = 0;
            int c = 0;
            int d = 0;
            for (int i = UAttack1.MoveInput.Count; i > 0; i--)
            {
                if (InputList[InputList.Count - i] == UAttack1.MoveInput[d])
                    d++;

                if (d == UAttack1.MoveInput.Count)
                {
                    _animator.SetTrigger("ShinkuuHadoukenTrigger");
                    Debug.Log("U1");
                    InputList = new List<string> { "None" };
                    return true;
                }
            }

            for (int i = SpAttack1.MoveInput.Count; i > 0; i--)
            {
                

                if (InputList[InputList.Count - i] == SpAttack1.MoveInput[a])
                    a++;

                if (a == SpAttack1.MoveInput.Count)
                {
                    _animator.SetTrigger("HadoukenTrigger");
                    Debug.Log("Sp1");
                    InputList = new List<string> { "None" };
                    return true;
                }
                if (InputList[InputList.Count - i] == SpAttack2.MoveInput[b])
                    b++;

                if (b == SpAttack2.MoveInput.Count)
                {
                    _animator.SetTrigger("TatsumakiTrigger");
                    Debug.Log("Sp2");
                    InputList = new List<string> { "None" };
                    return true;
                }
                if (InputList[InputList.Count - i] == SpAttack3.MoveInput[c])
                    c++;

                if (c == SpAttack3.MoveInput.Count)
                {
                    _animator.SetTrigger("ShoryukenTrigger");
                    Debug.Log("Sp3");
                    InputList = new List<string> { "None" };
                    return true;
                }
            }
            
        }
        return false;
    }
}
