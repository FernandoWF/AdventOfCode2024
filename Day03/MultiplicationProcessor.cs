namespace AdventOfCode2024.Day03
{
    internal class MultiplicationProcessor()
    {
        private enum State
        {
            Empty,
            M,
            U,
            L,
            OpeningParenthesis,
            FirstNumberFirstDigit,
            FirstNumberSecondDigit,
            FirstNumberThirdDigit,
            Comma,
            SecondNumberFirstDigit,
            SecondNumberSecondDigit,
            SecondNumberThirdDigit,
            ClosingParenthesis
        }

        private State state;
        private string firstNumber = string.Empty;
        private string secondNumber = string.Empty;

        public int? Result { get; private set; }

        public void Process(char character)
        {
            Result = null;

            state = state switch
            {
                State.Empty when character == 'm' => State.M,
                State.M when character == 'u' => State.U,
                State.U when character == 'l' => State.L,
                State.L when character == '(' => State.OpeningParenthesis,
                State.OpeningParenthesis when char.IsAsciiDigit(character) => State.FirstNumberFirstDigit,

                State.FirstNumberFirstDigit when char.IsAsciiDigit(character) => State.FirstNumberSecondDigit,
                State.FirstNumberFirstDigit when character == ',' => State.Comma,

                State.FirstNumberSecondDigit when char.IsAsciiDigit(character) => State.FirstNumberThirdDigit,
                State.FirstNumberSecondDigit when character == ',' => State.Comma,

                State.FirstNumberThirdDigit when character == ',' => State.Comma,

                State.Comma when char.IsAsciiDigit(character) => State.SecondNumberFirstDigit,

                State.SecondNumberFirstDigit when char.IsAsciiDigit(character) => State.SecondNumberSecondDigit,
                State.SecondNumberFirstDigit when character == ')' => State.ClosingParenthesis,

                State.SecondNumberSecondDigit when char.IsAsciiDigit(character) => State.SecondNumberThirdDigit,
                State.SecondNumberSecondDigit when character == ')' => State.ClosingParenthesis,

                State.SecondNumberThirdDigit when character == ')' => State.ClosingParenthesis,

                _ => State.Empty,
            };

            Action postTransitionAction = state switch
            {
                State.FirstNumberFirstDigit => () => firstNumber += character,
                State.FirstNumberSecondDigit => () => firstNumber += character,
                State.FirstNumberThirdDigit => () => firstNumber += character,

                State.SecondNumberFirstDigit => () => secondNumber += character,
                State.SecondNumberSecondDigit => () => secondNumber += character,
                State.SecondNumberThirdDigit => () => secondNumber += character,

                State.ClosingParenthesis => () =>
                {
                    Result = int.Parse(firstNumber) * int.Parse(secondNumber);
                    state = State.Empty;
                    firstNumber = secondNumber = string.Empty;
                },

                State.Empty => () => firstNumber = secondNumber = string.Empty,

                _ => () => { }
            };
            postTransitionAction();
        }
    }
}
