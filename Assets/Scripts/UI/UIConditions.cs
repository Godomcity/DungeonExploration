using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConditions : MonoBehaviour
{
    public Condition health;

    void Start()
    {

        CharacterManager.Instance.Player.conditions.uiCondition = this;
    }
}
