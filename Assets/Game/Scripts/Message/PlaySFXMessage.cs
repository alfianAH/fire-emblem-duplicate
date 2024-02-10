namespace FireEmblemDuplicate.Message
{
    public struct PlaySFXMessage
    {
        public string AudioName { get; private set; }

        public PlaySFXMessage(string audioName)
        {
            AudioName = audioName;
        }
    }
}