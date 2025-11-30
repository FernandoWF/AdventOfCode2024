namespace AdventOfCode2024.Day16
{
    internal record Tile(Position Position)
    {
        public Direction? EntryDirection { get; set; }

        public virtual bool Equals(Tile? other)
        {
            return Position.Equals(other?.Position);
        }

        public override int GetHashCode()
        {
            return Position.GetHashCode();
        }
    }
}
