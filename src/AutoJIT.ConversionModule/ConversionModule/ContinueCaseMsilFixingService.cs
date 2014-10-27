using System.Collections.Generic;
using System.IO;
using System.Linq;
using Mono.Cecil;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace AutoJIT.CSharpConverter.ConversionModule
{
    public class ContinueCaseMsilFixingService : IContinueCaseMsilFixingService
    {
        public byte[] Fix( byte[] assembly, string scriptClassName ) {
            var assemblyStream = new MemoryStream( assembly );
            AssemblyDefinition assemblyDefinition = AssemblyDefinition.ReadAssembly( assemblyStream );
            TypeDefinition typeToManipulate = assemblyDefinition.MainModule.Types.Single( x => x.Name.Equals( scriptClassName ) );

            foreach (MethodDefinition methodDefinition in typeToManipulate.Methods) {
                methodDefinition.Body.SimplifyMacros();

                ILProcessor ilProcessor = methodDefinition.Body.GetILProcessor();

                IEnumerable<Instruction> continueCases = GetContinueCaseInstructions( methodDefinition.Body.Instructions );

                List<Instruction> continueDestinations = GetContinueCaseDestinations( methodDefinition.Body.Instructions );

                foreach (Instruction continueCase in continueCases) {
                    Instruction toNop1 = methodDefinition.Body.Instructions[methodDefinition.Body.Instructions.IndexOf( continueCase )+1];
                    NopInstruction( toNop1, ilProcessor );
                    
                    
                    Instruction jumpDestination = GetContinueDestination( continueDestinations, continueCase );
                    int jumpDestinationIndex = methodDefinition.Body.Instructions.IndexOf( jumpDestination );

                    Instruction toNop2 = methodDefinition.Body.Instructions[jumpDestinationIndex+1];
                    NopInstruction( toNop2, ilProcessor );
                    NopInstruction( jumpDestination, ilProcessor );

                    Instruction instruction = methodDefinition.Body.Instructions[jumpDestinationIndex];

                    Instruction jumpInstruction = ilProcessor.Create( OpCodes.Br, instruction );
                    continueCase.OpCode = jumpInstruction.OpCode;
                    continueCase.Operand = jumpInstruction.Operand;
                }

                IEnumerable<Instruction> unusedTargets = GetContinueCaseDestinations( methodDefinition.Body.Instructions );
                foreach (var unusedTarget in unusedTargets) {
                    var unusedIndex = methodDefinition.Body.Instructions.IndexOf( unusedTarget ) + 1;
                    var cwlCall = methodDefinition.Body.Instructions[unusedIndex];
                    NopInstruction( cwlCall, ilProcessor );
                    NopInstruction( unusedTarget, ilProcessor );
                }

                methodDefinition.Body.OptimizeMacros();
            }

            var toReturn = new MemoryStream();
            assemblyDefinition.Write( toReturn );
            return toReturn.ToArray();
        }

        private static void NopInstruction( Instruction toNop, ILProcessor ilProcessor ) {
            var instruction = ilProcessor.Create( OpCodes.Nop );
            toNop.OpCode = instruction.OpCode;
            toNop.Operand = instruction.Operand;
        }

        private Instruction GetContinueDestination( IEnumerable<Instruction> continueDestinations, Instruction continueCase ) {
            return continueDestinations.Single(
                x => x.Operand != null && ( (string) x.Operand )
                    .EndsWith( ( (string) continueCase.Operand )
                        .Replace( "JUMPTOHACK", string.Empty ) ) );
        }

        private static List<Instruction> GetContinueCaseDestinations( IEnumerable<Instruction> instructions ) {
            return instructions.Where( x => x.OpCode == OpCodes.Ldstr )
                .Where( x => ( (string) x.Operand ).StartsWith( "JUMPABHACK" ) )
                .ToList();
        }

        private static IEnumerable<Instruction> GetContinueCaseInstructions( IEnumerable<Instruction> instructions ) {
            return instructions.Where( x => x.OpCode == OpCodes.Ldstr )
                .Where( x => ( (string) x.Operand ).StartsWith( "JUMPTOHACK" ) )
                .ToList();
        }
    }
}
