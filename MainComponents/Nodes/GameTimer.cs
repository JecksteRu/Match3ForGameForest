using System;
using Microsoft.Xna.Framework;

namespace Match3Game.Node
{
    public sealed class GameTimer : UILabel
    {
        public event Action TimeEnded;
        
        private const float GamePlayTime = 60f;
        
        public float RemainTime = GamePlayTime;


        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            RemainTime -= (float)gameTime.ElapsedGameTime.TotalSeconds;
            SetText(MathF.Round(RemainTime, 2).ToString());
            if (RemainTime <= 0)
            {
                TimeEnded?.Invoke();
                TimeEnded = null;
                Dispose();
            }
        }
    }


}



