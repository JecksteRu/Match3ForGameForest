using System;


namespace Match3Game.Node
{
    public sealed class PlayerScoreSystem : UILabel
    {
        public override void LoadContent()
        {
            base.LoadContent();
        
            SetText("Your current score is " + CurrentScore.ToString());
        }

        public int CurrentScore { get; private set; }

        public void AddScore(int amount)
        {
            CurrentScore += amount;
            SetText("Your current score is " + CurrentScore.ToString());
        }

        public void Reset()
        {
            CurrentScore = 0;
        }



    }
}

