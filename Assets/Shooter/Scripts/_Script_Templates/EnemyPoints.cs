using UnityEngine;
using System.Collections.Generic;
using System.IO;

public static class EnemyPoints
{
    private static Dictionary<string, int> enemyPoints = new Dictionary<string, int>();

    public static void InitializeFromTextFile(string filePath)
    {
        enemyPoints.Clear();

        if (!File.Exists(filePath))
        {
            Debug.LogError("Enemy point data file not found: " + filePath);
            return;
        }

        string[] lines = File.ReadAllLines(filePath);
        foreach (string line in lines)
        {
            string[] parts = line.Split(',');
            if (parts.Length == 2)
            {
                string enemyName = parts[0];
                int points;
                if (int.TryParse(parts[1], out points))
                {
                    enemyPoints[enemyName] = points;
                }
                else
                {
                    Debug.LogError("Invalid points data for enemy: " + line);
                }
            }
            else
            {
                Debug.LogError("Invalid line format in enemy point data: " + line);
            }
        }
    }

    public static string GetPoints(string enemyName)
    {
        if (enemyPoints.ContainsKey(enemyName))
        {
            return enemyPoints[enemyName].ToString();
        }
        else
        {
            // Default points if enemy name not found
            return 0.ToString();
        }
    }
}
