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
    public Effect SpAttack2;
    public GameObject Projectile;
    public float CurrentHealth;
    public float HealthAmount;
    public float Speed;
    public Transform opponentTransform;
    private float TimeSinceLastInput = 0;
    private HealthScriptable Health;
    private List<string> InputList = new List<string> { "None" };
    private Rigidbody2D rb2d;
    private FighterState CurrentState;
    private Animator _animator;
    private float RightButtonCooldown = 0.5f;
    private float RightButtonCount = 0;
    private float LeftButtonCooldown = 0.5f;
    private float LeftButtonCount = 0;

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
        if(opponentTransform.position.x < this.transform.position.x && transform.rotation.y != 180)
        {
            var rotation = new Quaternion(0, 180, 0, 0);
            transform.rotation = rotation; 
        }
        else
        {
            var rotation = new Quaternion(0, 0, 0, 0);
            transform.rotation = rotation;
        }
        if (InputList.Count > 4)
        {
            int a = 0;
            for (int i = SpAttack1.InputCount; i > 0; i--)
            {
                if (InputList[InputList.Count - i] != SpAttack1.MoveInput[a])
                    break;
                else
                    a++;

                if (a == 4)
                {
                    SpAttack1.DoEffect(this.transform.position, Projectile, rb2d);
                    InputList.Add("Sp1");
                    Debug.Log("Sp1");
                    InputList = new List<string> { "None" };
                }
            }
            int b = 0;
            for (int i = SpAttack2.InputCount; i > 0; i--)
            {
                if (InputList[InputList.Count - i] != SpAttack2.MoveInput[b])
                    break;
                else
                    b++;

                if (b == 4)
                {
                    SpAttack2.DoEffect(this.transform.position, Projectile, rb2d);
                    InputList.Add("Sp2");
                    Debug.Log("Sp2");
                    InputList = new List<string> { "None" };
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            if (RightButtonCooldown > 0 && RightButtonCount == 1)
            {
                rb2d.AddForce(new Vector2((Speed + 25) * 10, 0));
                InputList.Add("DashForward");
                Debug.Log("DashForward");
            }
            else
            {
                RightButtonCooldown = 0.5f;
                RightButtonCount += 1;
            }
        }
        if (RightButtonCooldown > 0)
            RightButtonCooldown -= 1 * Time.deltaTime;
        else
            RightButtonCount = 0;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            if (LeftButtonCooldown > 0 && LeftButtonCount == 1)
            {
                rb2d.AddForce(new Vector2(-1 * ((Speed + 25) * 10), 0));
                InputList.Add("DashBackward");
                Debug.Log("DashBackward");
            }
            else
            {
                LeftButtonCooldown = 0.5f;
                LeftButtonCount += 1;
            }
        }
        if (LeftButtonCooldown > 0)
            LeftButtonCooldown -= 1 * Time.deltaTime;
        else
            LeftButtonCount = 0;

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
        {
            if (InputList[InputList.Count - 1] != "DownForward")
            {
                InputList.Add("DownForward");
                Debug.Log("DownForward");
            }
            TimeSinceLastInput = 0;
        }

        if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
        {
            if (InputList[InputList.Count - 1] != "DownBack")
            {
                InputList.Add("DownBack");
                Debug.Log("DownBack");
            }
            TimeSinceLastInput = 0;
        }

        else if (Input.GetKey(KeyCode.DownArrow))
        {
            InputList.Add("Down");
            Debug.Log("Down");
            UpdateState(FighterState.Crouching);
            _animator.SetInteger("State", 4);
            TimeSinceLastInput = 0;
        }

        else if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            UpdateState(FighterState.Idle);
        }

        else if (Input.GetKey(KeyCode.RightArrow))
        {
            if (this.transform.rotation.y == 0)
            {
                if (InputList[InputList.Count - 1] != "Forward")
                {
                    InputList.Add("Forward");
                    Debug.Log("Forward");
                }
                UpdateState(FighterState.WalkingForward);
                _animator.SetInteger("State", 1);
                rb2d.AddForce(new Vector2(transform.right.x * Speed, 0));
            }
            else
            {
                if (InputList[InputList.Count - 1] != "Back")
                {
                    InputList.Add("Back");
                    Debug.Log("Back");
                }
                UpdateState(FighterState.WalkingBackward);
                _animator.SetInteger("State", 2);
                rb2d.AddForce(new Vector2(-(transform.right.x * Speed), 0));
                TimeSinceLastInput = 0;
            }
            TimeSinceLastInput = 0;
        }

        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (this.transform.rotation.y == 0)
            {
                if (InputList[InputList.Count - 1] != "Back")
                {
                    InputList.Add("Back");
                    Debug.Log("Back");
                }
                UpdateState(FighterState.WalkingBackward);
                _animator.SetInteger("State", 2);
                rb2d.AddForce(new Vector2(-(transform.right.x * Speed), 0));
            }
            else
            {
                if (InputList[InputList.Count - 1] != "Forward")
                {
                    InputList.Add("Forward");
                    Debug.Log("Forward");
                }
                UpdateState(FighterState.WalkingForward);
                _animator.SetInteger("State", 1);
                rb2d.AddForce(new Vector2(transform.right.x * Speed, 0));
            }
            TimeSinceLastInput = 0;
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            InputList.Add("Attack");
            Debug.Log("Attack");
            UpdateState(FighterState.LightPunch);
            _animator.SetInteger("State", 5);
            TimeSinceLastInput = 0;
        }

        else if (Input.GetKeyDown(KeyCode.S))
        {
            InputList.Add("Attack");
            Debug.Log("Attack");
            UpdateState(FighterState.MediumPunch);
            _animator.SetInteger("State", 6);
            TimeSinceLastInput = 0;
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            InputList.Add("Attack");
            Debug.Log("Attack");
            UpdateState(FighterState.HeavyPunch);
            _animator.SetInteger("State", 7);
            TimeSinceLastInput = 0;
        }

        else
        {
            UpdateState(FighterState.Idle);
            _animator.SetInteger("State", 0);
        }
        TimeSinceLastInput += Time.deltaTime;
        
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