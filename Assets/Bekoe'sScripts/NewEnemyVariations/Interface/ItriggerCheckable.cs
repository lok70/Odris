using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ItriggerCheckable
{
    bool isInShootingDistance { get; set; }
    bool isInChasingDistance { get; set; }
    bool isAggroed { get; set; }
   

    void SetAggroStatus(bool isAggroed);
    void CheckChasingDistance(bool isInChasingDistance);
    void CheckShootingDistance(bool isInShootingDistance);
}
