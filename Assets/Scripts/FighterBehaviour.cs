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
        LightPunch, //State 5
        MediumPunch, //State 6
        HeavyPunch //State 7
    }

    public Effect SpAttack1;
    public GameObject Projectile;
    public float CurrentHealth;
    public float HealthAmount;
    public float Speed;
    public float InputTimer;
    private float InputTimerReset;
    private HealthScriptable Health;
    private List<string> InputList = new List<string> { };
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
        InputTimerReset = InputTimer;
    }
    // Update is called once per frame
    void Update ()
    {
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
                    InputList = new List<string> { };
                }
            }
            if (InputList[InputList.Count - 2] == "Right" && InputList[InputList.Count - 1] == "Right")
            {
                rb2d.AddForce(new Vector2(Speed*10, 0));
                InputList.Add("DashForward");
            }
            if (InputList[InputList.Count - 2] == "Left" && InputList[InputList.Count - 1] == "Left")
            {
                rb2d.AddForce(new Vector2(-1*(Speed * 10), 0));
                InputList.Add("DashBackward");
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
            InputList.Add("Down");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
            UpdateState(FighterState.Crouching);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            InputList.Add("Right");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
            UpdateState(FighterState.WalkingForward);
            _animator.SetInteger("State", 1);
            rb2d.AddForce(new Vector2(transform.right.x*Speed, 0));
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (InputList[InputList.Count - 1] != "Right")
            {
                InputList.Add("Right");
            }
            UpdateState(FighterState.WalkingForward);
            _animator.SetInteger("State", 1);
            rb2d.AddForce(new Vector2(transform.right.x * Speed, 0));
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            InputList.Add("Left");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
            UpdateState(FighterState.WalkingBackward);
            _animator.SetInteger("State", 2);
            rb2d.AddForce(new Vector2(-(transform.right.x*Speed), 0));
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (InputList[InputList.Count - 1] != "Left")
            {
                InputList.Add("Left");
            }
            UpdateState(FighterState.WalkingBackward);
            _animator.SetInteger("State", 2);
            rb2d.AddForce(new Vector2(-(transform.right.x * Speed), 0));
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            InputList.Add("Attack");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
            UpdateState(FighterState.LightPunch);
            _animator.SetInteger("State", 5);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            InputList.Add("Attack");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
            UpdateState(FighterState.MediumPunch);
            _animator.SetInteger("State", 6);
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            InputList.Add("Attack");
            foreach (var input in InputList)
            {
                Debug.Log(input);
            }
            UpdateState(FighterState.HeavyPunch);
            _animator.SetInteger("State", 7);
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
        CurrentHealth = Health.Value;
    }

    public void UpdateState(FighterState newState)
    {
        CurrentState = newState;
    }
}
