using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// This editor is used to display the radius of an explosion attack
// of a character with a BossAI script attached in the Scene window
//
// When the boss character uses an explosion attack
// this script also draws a line between the boss and an unblocked target object
[CustomEditor(typeof(BossAI))]
public class ExplosionEditor : Editor
{
    private void OnSceneGUI()
    {
        BossAI boss = (BossAI)target;
        Handles.color = Color.blue;
        Handles.DrawWireArc(boss.transform.position, Vector3.up, Vector3.forward, 360, boss.MaximumExplosionRadius);

        Handles.color = Color.red;
        foreach (Transform visibleTarget in boss.OpenTargets)
        {
            Handles.DrawLine(boss.transform.position, visibleTarget.position);
        }
    }
}