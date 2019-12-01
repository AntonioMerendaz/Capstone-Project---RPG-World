﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : EntityController
{
    // Variable
    protected bool moving;
    public float timeBetweenMove;
    protected float timeBetweenMoveCounter;
    public float timeToMove;
    protected float timeToMoveCounter;
    protected Vector3 moveDirection;
    protected PlayerController thePlayer;
    public EnemyType type;

    // Start is called before the first frame update
    protected override  void Start()
    {
        this.myRigidBody = GetComponent<Rigidbody2D>();

        this.RenewTimeBetweenMoveCounter();
        this.RenewTimeToMoveCounter();

        this.thePlayer = FindObjectOfType<PlayerController>();

        this.InitStats();
        this.AfterStart();
        base.UpdateObjects();

        this.GetStats().SetEnemyType(this.type);

        this.GetStats().SetName(this.NameString());
    }

    // Update is called once per frame
    protected override  void Update()
    {
        this.Move();
        this.AfterUpdate();
        this.stats.Update();
    }

    protected void RenewTimeBetweenMoveCounter()
    {
        this.timeBetweenMoveCounter = Random.Range(this.timeBetweenMove * 0.5f, this.timeBetweenMove * 1.75f);
    }

    protected void RenewTimeToMoveCounter()
    {
        this.timeToMoveCounter = Random.Range(this.timeToMove * 0.5f, this.timeToMove * 1.75f);
    }

    protected void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.name == "Player")
        {
            this.Attack();
        }
    }
    
    protected void Attack()
    {
        this.stats.TryAttack(this.thePlayer.GetStats());
    }

    public override string NameString()
    {
        switch(this.type)
        {
            case EnemyType.Slime: return "Slime";
        }
        return "";
    }

    protected abstract void AfterStart();
    protected abstract void AfterUpdate();
    protected abstract void Move();
    protected abstract void InitStats();
}