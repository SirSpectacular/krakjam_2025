namespace Game.Model
{
    public class GameState
    {
        public bool IsStarted { get; private set; }
        public bool IsFinished { get; private set; }
        public int? WinnerId;
        public bool IsWin => WinnerId != null;

        public void Start()
        {
            if (IsStarted)
            {
                return;
            }
            
            IsStarted = true;
        }

        public void Finish(int? playerId)
        {
            if (IsFinished)
            {
                return;
            }
            
            IsFinished = true;
            WinnerId = playerId;
        }
    }
}