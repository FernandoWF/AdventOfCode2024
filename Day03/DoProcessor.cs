namespace AdventOfCode2024.Day03
{
    internal class DoProcessor()
    {
        private enum State
        {
            Empty,
            D,
            O,
            OpeningParenthesis,
            ClosingParenthesis
        }

        private State state;

        public bool Finished => state == State.ClosingParenthesis;

        public void Process(char character)
        {
            state = state switch
            {
                State.Empty when character == 'd' => State.D,
                State.D when character == 'o' => State.O,
                State.O when character == '(' => State.OpeningParenthesis,
                State.OpeningParenthesis when character == ')' => State.ClosingParenthesis,

                _ => State.Empty,
            };
        }
    }
}
