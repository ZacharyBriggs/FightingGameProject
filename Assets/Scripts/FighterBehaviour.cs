﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class FighterBehaviour : MonoBehaviour, IDamageable
{
    public enum FighterState
    {
        Idle, //State 0
        WalkingForward, //State 1
        WalkingBackward, //State 2
        Jumping, //State 3
        Crouching, //State 4
    }

    public float CurrentHealth;
    public float HealthAmount;
    public float MeterAmount;
    public float Speed;
    public Transform opponentTransform;
    private float TimeSinceLastInput = 0;
    private HealthScriptable Health;
    [NonSerialized]
    public MeterScriptable Meter;
    public bool Player2;
    protected List<string> InputList = new List<string> { "None" };
    private Rigidbody2D rb2d;
    private FighterState CurrentState;
    protected Animator _animator;
    private float RightButtonCooldown = 0.5f;
    private float RightButtonCount = 0;
    private float LeftButtonCooldown = 0.5f;
    private float LeftButtonCount = 0;
    private bool IsGrounded = true;

    protected void StartUp()
    {
        Health = ScriptableObject.CreateInstance<HealthScriptable>();
        Health.MaxValue = HealthAmount;
        Health.Value = HealthAmount;
        Meter = ScriptableObject.CreateInstance<MeterScriptable>();
        Meter.MaxValue = 100;
        Meter.Value = 0;
        CurrentHealth = HealthAmount;
        rb2d = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    protected void RotationCheck()
    {
        if (opponentTransform.position.x < this.transform.position.x && transform.rotation.y != 180)
        {
            var rotation = new Quaternion(0, 180, 0, 0);
            transform.rotation = rotation;
        }
        else
        {
            var rotation = new Quaternion(0, 0, 0, 0);
            transform.rotation = rotation;
        }
    }
    protected void DoubleTapCheck()
    {
        if ( Player2 == false && Input.GetButtonDown("Right") 
            || Player2 == true && Input.GetButtonDown("P2Right"))
        {

            if (RightButtonCooldown > 0 && RightButtonCount == 1)
            {
                if (InputList[InputList.Count - 1] == "Forward")
                {
                    rb2d.AddForce(new Vector2(transform.right.x*Speed+25,0));
                    InputList.Add("DashForward");
                    Debug.Log("DashForward");
                }
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

        if (Player2 == false && Input.GetButtonDown("Left") 
            || Player2 == true && Input.GetButtonDown("P2Left"))
        {

            if (LeftButtonCooldown > 0 && LeftButtonCount == 1)
            {
                if (InputList[InputList.Count - 1] == "Back")
                {
                    rb2d.AddForce(new Vector2(-1 * ((Speed + 25) * 10), 0));
                    InputList.Add("DashBackward");
                    Debug.Log("DashBackward");
                }
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
    }
    protected void CheckButtons()
    {
        if (Player2 == false && Input.GetButton("Down") && Input.GetButton("Right") 
            || Player2 == true && Input.GetButton("P2Down") && Input.GetButton("Right"))
        {
            if (this.transform.rotation.y == 0)
            {
                if (InputList[InputList.Count - 1] != "DownForward")
                {
                    InputList.Add("DownForward");
                    Debug.Log("DownForward");
                }
            }
            else
            {
                if (InputList[InputList.Count - 1] != "DownBack")
                {
                    InputList.Add("DownBack");
                    Debug.Log("DownBack");
                }
            }
            TimeSinceLastInput = 0;
        }

        if (Player2 == false && Input.GetButton("Down") && Input.GetButton("Left")
            || Player2 == true && Input.GetButton("P2Down") && Input.GetButton("Left"))
        {
            if (this.transform.rotation.y == 0)
            {
                if (InputList[InputList.Count - 1] != "DownBack")
                {
                    InputList.Add("DownBack");
                    Debug.Log("DownBack");
                }
            }
            else
            {
                if (InputList[InputList.Count - 1] != "DownForward")
                {
                    InputList.Add("DownForward");
                    Debug.Log("DownForward");
                }
            }
            TimeSinceLastInput = 0;
        }

        else if (Player2 == false && Input.GetButton("Down") && !Input.GetButton("Right")
                || Player2 == true && Input.GetButton("P2Down") && !Input.GetButton("Right"))
        {
            if (InputList[InputList.Count - 1] != "Down")
            {
                InputList.Add("Down");
                Debug.Log("Down");
            }
            UpdateState(FighterState.Crouching);
            _animator.SetInteger("State", 4);
            TimeSinceLastInput = 0;
        }

        else if (Player2 == false && Input.GetButtonUp("Down")
            || Player2 == true && Input.GetButtonUp("P2Down"))
        {
            UpdateState(FighterState.Idle);
        }

        else if (Player2 == false && Input.GetButtonDown("Up") 
            || Player2 == true && Input.GetButtonDown("P2Up"))
        {
            if (IsGrounded == true)
            {
                InputList.Add("Up");
                Debug.Log("Up");
                UpdateState(FighterState.Jumping);
                _animator.SetInteger("State", 3);
                rb2d.AddForce(new Vector2(0, 2500));
                TimeSinceLastInput = 0;
            }
        }

        else if (Player2 == false && Input.GetButton("Right") && !Input.GetButton("Down") 
            || Player2 == true && Input.GetButton("P2Right") && !Input.GetButton("P2Down"))
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

        else if (Player2 == false && Input.GetButton("Left") && !Input.GetButton("Down")
            || Player2 == true &&Input.GetButton("P2Left") && !Input.GetButton("P2Down"))
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

        else if (Player2 == false && Input.GetButtonDown("Attack1")
            || Player2 == true && Input.GetButtonDown("P2Attack1"))
        {
            InputList.Add("Attack");
            Debug.Log("Attack");
            if(!CheckInput())
                _animator.SetTrigger("LightAttackTrigger");
            TimeSinceLastInput = 0;
        }

        else if (Player2 == false && Input.GetButtonDown("Attack2")
            || Player2 == true && Input.GetButtonDown("P2Attack2"))
        {
            InputList.Add("Attack");
            Debug.Log("Attack");
            if (!CheckInput())
                _animator.SetTrigger("MediumAttackTrigger");
            TimeSinceLastInput = 0;
        }

        else if (Player2 == false && Input.GetButtonDown("Attack3")
            || Player2 == true && Input.GetButtonDown("P2Attack3"))
        {
            InputList.Add("Attack");
            Debug.Log("Attack");
            if (!CheckInput())
                _animator.SetTrigger("HeavyAttackTrigger");
            TimeSinceLastInput = 0;
        }

        else
        {
            UpdateState(FighterState.Idle);
            _animator.SetInteger("State", 0);
        }

        TimeSinceLastInput += Time.deltaTime;
        if (transform.position.y > 0)
            IsGrounded = false;

        else if (transform.position.y < -0.5)
            IsGrounded = true;
        _animator.SetBool("IsGrounded", IsGrounded);
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

    public abstract bool CheckInput();

    public void AddForceOnX(float x)
    {
        rb2d.AddForce(new Vector2(x,0));
    }
    public void AddForceOnY(float y)
    {
        rb2d.AddForce(new Vector2(0, y));
    }
}