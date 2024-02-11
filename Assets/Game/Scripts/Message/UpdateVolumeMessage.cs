namespace FireEmblemDuplicate.Message
{
    public struct UpdateVolumeMessage
    {
        public string VolumeType { get; private set; }
        public float Volume { get; private set; }

        public UpdateVolumeMessage(string volumeType, float volume)
        {
            VolumeType = volumeType;
            Volume = volume;
        }
    }
}
