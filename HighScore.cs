using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace SnakeGame1
{
    public class HighScoreManager
    {
        private const string FileName = "highscores.json";

        public void SaveHighScore(int score, string gameMode)
        {
            var highScores = LoadHighScores();
            if (!highScores.ContainsKey(gameMode) || highScores[gameMode] < score)
            {
                highScores[gameMode] = score;
                File.WriteAllText(FileName, JsonConvert.SerializeObject(highScores));
            }
        }

        public int GetHighScore(string gameMode)
        {
            var highScores = LoadHighScores();
            return highScores.ContainsKey(gameMode) ? highScores[gameMode] : 0;
        }

        private Dictionary<string, int> LoadHighScores()
        {
            if (File.Exists(FileName))
            {
                var json = File.ReadAllText(FileName);
                return JsonConvert.DeserializeObject<Dictionary<string, int>>(json) ?? new Dictionary<string, int>();
            }
            return new Dictionary<string, int>();
        }
    }
}