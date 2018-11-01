using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FighterBehaviour : MonoBehaviour
{
    public Effect SpAttack1;
    public GameObject Projectile;
    public float CurrentHealth;
    public float HealthAmount;
    private HealthScriptable Health;
    private List<string> InputList = new List<string> { "None"};
    private CharacterController controller;

    private void Start()
    {
        Health = ScriptableObject.CreateInstance<HealthScriptable>();
        Health.MaxValue = HealthAmount;
        Health.Value = HealthAmount;
        CurrentHealth = HealthAmount;
        controller = GetComponent<CharacterController>();
    }
    // Update is called once per frame
    void Update ()
    {
        CurrentHealth = Health.Value;
        if (InputList.Count > 4)
        {
            int j = 0;
            for (int i = 4; i > 0; i--)
            {
                if (InputList[InputList.Count - i] != SpAttack1.MoveInput[j])
                    break;
                else
                    j++;

                if (j == 4)
                {
                    SpAttack1.DoEffect(this.transform.position, Projectile);
                    InputList.Add("Hadouken");
                }

            }
        }

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (InputList[InputList.Count - 1] != "DownRight")
            {
                InputList.Add("DownRight");
                foreach (var input in InputList)
                {
                    Debug.Log(input);
                }
            }
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (InputList[InputList.Count - 1] != "Down")
            {
                InputList.Add("Down");
                foreach (var input in InputList)
                {
                    Debug.Log(input);
                }
            }
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (InputList[InputList.Count - 1] != "Right")
            {
                InputList.Add("Right");
                foreach (var input in InputList)
                {
                    Debug.Log(input);
                }
            }
            controller.SimpleMove(transform.right);
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (InputList[InputList.Count - 1] != "Left")
            {
                InputList.Add("Left");
                foreach (var input in InputList)
                {
                    Debug.Log(input);
                }
            }
            controller.SimpleMove(-transform.right);
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
