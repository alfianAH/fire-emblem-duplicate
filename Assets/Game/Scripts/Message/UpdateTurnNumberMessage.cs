namespace FireEmblemDuplicate.Message
{
    public struct UpdateTurnNumberMessage
    {
        public int TurnNumber { get; private set; }
        
        public UpdateTurnNumberMessage(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }
}