using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUnit
{
    public void StartTurn();
    public void EndTurn();
    public void PlayHitAnimation();
    public void PlayMissAnimation();
    public void PlayWinAnimation();
    public void PlayLoseAnimation();
    public IEnumerator PlayHurtAnimation();
    public void PlayDogeAnimation();
}
