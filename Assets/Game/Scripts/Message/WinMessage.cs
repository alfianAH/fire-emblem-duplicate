using FireEmblemDuplicate.Scene.Battle.Unit.Enum;

namespace FireEmblemDuplicate.Message
{
    public struct WinMessage
    {
        public UnitSide WinningSide { get; private set; }

        public WinMessage(UnitSide winningSide)
        {
            WinningSide = winningSide;
        }
    }
}