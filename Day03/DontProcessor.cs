namespace AdventOfCode2024.Day03
{
    internal class DontProcessor()
    {
        private enum State
        {
            Empty,
            D,
            O,
            N,
            Apostrophe,
            T,
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
                State.O when character == 'n' => State.N,
                State.N when character == '\'' => State.Apostrophe,
                State.Apostrophe when character == 't' => State.T,
                State.T when character == '(' => State.OpeningParenthesis,
                State.OpeningParenthesis when character == ')' => State.ClosingParenthesis,

                _ => State.Empty,
            };
        }
    }
}
