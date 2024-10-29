using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageble
{
    void TakePhysicalDamage(int damageAmount);
}

public class PlayerConditions : MonoBehaviour, IDamageble
{
    public UIConditions uiCondition;

    Condition health { get { return uiCondition.health; } }

    public event Action onTakeDamage;

    private void Update()
    {
        health.Subtract(health.passiveValue * Time.deltaTime);

        if (health.curValue <= 0.0f)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Die()
    {
        Debug.Log("플레이어가 죽었다");
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Subtract(damageAmount);
        onTakeDamage?.Invoke();
    }
}
