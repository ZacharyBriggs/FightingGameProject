using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FighterBehaviour : MonoBehaviour, IDamageable
{
    public enum FighterState
    {
        Idle, //State 0
        WalkingForward, //State 1
        WalkingBackward, //State 2
        Jumping, //State 3
        Crouching, //State 4
        LightPunch //State 5
    }

    public Effect SpAttack1;
    public GameObject Projectile;
    public float CurrentHealth;
    public float HealthAmount;
    private HealthScriptable Health;
    private List<string> InputList = new List<string> { "None"};
    private Rigidbody2D rb2d;
    private FighterState CurrentState;
    private Animator _animator;

    private void Start()
    {
        Health = ScriptableObject.CreateInstance<HealthScriptable>();
        Health.MaxValue = HealthAmount;
        Health.Value = HealthAmount;
        CurrentHealth = HealthAmount;
        rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
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
            UpdateState(FighterState.Crouching);
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
            UpdateState(FighterState.WalkingForward);
            _animator.SetInteger("State", 1);
            rb2d.AddForce(new Vector2(transform.right.x*10, 0));
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
            UpdateState(FighterState.WalkingBackward);
            _animator.SetInteger("State", 2);
            rb2d.AddForce(new Vector2(-(transform.right.x*10), 0));
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            InputList.Add("Attack");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
            UpdateState(FighterState.LightPunch);
            _animator.SetInteger("State", 5);
        }

        else
        {
            UpdateState(FighterState.Idle);
            _animator.SetInteger("State", 0);
        }
    }
    public void TakeDamage(float amount)
    {
        Health.TakeDamage(amount);
    }

    public void UpdateState(FighterState newState)
    {
        CurrentState = newState;
    }
}
