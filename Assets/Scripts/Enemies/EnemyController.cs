﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : EntityController
{
    // Variable
    protected PlayerController thePlayer;
    public EnemyType type;

    public string enemyQuestName;
    protected QuestManager questManager;

    // Start is called before the first frame update
    protected override  void Start()
    {
        base.Start();

        this.RenewwaitCounter();
        this.RenewmoveCounter();

        this.thePlayer = FindObjectOfType<PlayerController>();
        this.questManager = FindObjectOfType<QuestManager>();

        this.InitStats();
        this.AfterStart();
        base.UpdateObjects();

        this.GetStats().SetEnemyType(this.type);

        this.GetStats().SetName(this.Name());
        this.GetStats().SetFlashLength(.5f);
    }

    // Update is called once per frame
    protected override  void Update()
    {
        base.Update();
        this.Move();
        this.AfterUpdate();
        this.stats.Update();

        if(this.stats.GetCurrentHealth() <= 0)
        {
            this.questManager.enemyKilled = this.type;
            Destroy(this.gameObject);
        }
    }

    protected void RenewwaitCounter()
    {
        this.waitCounter = Random.Range(this.wait * 0.5f, this.wait * 1.75f);
    }

    protected void RenewmoveCounter()
    {
        this.moveCounter = Random.Range(this.move * 0.5f, this.move * 1.75f);
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

    public string Name()
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