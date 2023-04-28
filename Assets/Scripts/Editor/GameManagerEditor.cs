using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameManager gameManager = (GameManager)target;

        int highScore = GameData.LoadHighScore();
        EditorGUILayout.LabelField("Highscore", highScore.ToString());

        if (GUILayout.Button("Reset Highscore"))
        {
            gameManager.ResetHighScore();
        }
    }
}
