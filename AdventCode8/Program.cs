using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using AdventCode.Shared;

namespace AdventCode8
{
    public static class Program
    {
        public static void Main()
        {
            var input = AdventCodeHelpers.ReadInputFile();
            var instructions = input.Select(ParseInput).ToList();
            
            Console.WriteLine(Part1(instructions));
            Console.WriteLine(Part2(instructions));
        }

        private static int Part1(IReadOnlyList<Instruction> instructions)
        {
            var (_, accumulator) = ExecuteProgram(instructions);
            return accumulator;
        }
    
        private static int Part2(IReadOnlyList<Instruction> instructions)
        {
            for (var i = 0; i < instructions.Count; i++)
            {
                var instruction = instructions[0];
                if (instruction.Operation == Operation.Jmp || instruction.Operation == Operation.Nop)
                {
                    var inverseOperation = instruction.Operation == Operation.Jmp ? Operation.Nop : Operation.Jmp;

                    var newInstructions = instructions.ToList();
                    newInstructions[i] = new Instruction() {Operation = inverseOperation, Argument = instruction.Argument};

                    var (pointer, accumulator) = ExecuteProgram(newInstructions);

                    if (pointer == instructions.Count)
                    {
                        return accumulator;
                    }
                }
            }
            
            throw new Exception("Invalid program");
        }

        private static (int, int) ExecuteProgram(IReadOnlyList<Instruction> instructions)
        {
            var pointer = 0;
            var accumulator = 0;
            var stack = new Stack<Instruction>();

            while (true)
            {
                var instruction = instructions[pointer];

                if (stack.Contains(instruction))
                {
                    return (pointer, accumulator);
                }
                
                stack.Push(instruction);
                (pointer, accumulator) = instruction.Execute(pointer, accumulator);

                if (pointer == instructions.Count)
                {
                    return (pointer, accumulator);
                }
            }
        }

        private static Instruction ParseInput(string input)
        {
            var match = Regex.Match(input, @"([a-z]+) ([+|-]\d+)");
            return new Instruction()
            {
                Operation = match.Groups[1].Value,
                Argument = int.Parse(match.Groups[2].Value)
            };
        } 
    }
}