namespace AdventCode8
{
    public class Instruction
    {
        public string Operation { get; set; }
        
        public int Argument { get; set; }
        
        public (int, int) Execute(int pointer, int accumulator)
        {
            switch (Operation)
            {
                case AdventCode8.Operation.Acc:
                    accumulator += Argument;
                    pointer++;
                    break;
                case AdventCode8.Operation.Jmp:
                    pointer += Argument;
                    break;
                case AdventCode8.Operation.Nop:
                    pointer++;
                    break;
            }
            
            return (pointer, accumulator);
        }
    }
}