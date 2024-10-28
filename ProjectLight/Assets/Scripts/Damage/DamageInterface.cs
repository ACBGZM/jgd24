using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BombDamage
{
    public void OnBombHit(int damage); 
}

public interface BulletDamage
{
    public void OnBulletHit(int damage); 
}

public interface LaserDamage
{
    public void OnLaserHit(int damage); 
}

public interface BossDamage
{
    public void OnBossHit(int damage); 
  
}