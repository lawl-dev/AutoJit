using System.IO;
using System.Linq;
using Mono.Cecil.Cil;
using Mono.Cecil.Rocks;

namespace AutoJIT.Compiler
{
    public class ContinueCaseMsilFixingService : IContinueCaseMsilFixingService
    {
        public byte[] Fix( byte[] assembly, string scriptClassName ) {
            var assemblyStream = new MemoryStream(assembly);
            var assemblyDefinition = Mono.Cecil.AssemblyDefinition.ReadAssembly( assemblyStream );
            var typeToManipulate = assemblyDefinition.MainModule.Types.Single( x=>x .Name.Equals( scriptClassName ));

            foreach (var methodDefinition in typeToManipulate.Methods) {
                methodDefinition.Body.SimplifyMacros();
                
                var ilProcessor = methodDefinition.Body.GetILProcessor();

                
                var continueCases = methodDefinition.Body.Instructions.Where( x=>x.OpCode == OpCodes.Ldstr ).Where(x=>((string)x.Operand).StartsWith( "JUMPTOHACK" )).ToList();
                
                var targets = methodDefinition.Body.Instructions.Where(x => x.OpCode == OpCodes.Ldstr).Where(x => ((string)x.Operand).StartsWith("JUMPABHACK")).ToList();

                for ( int index = 0; index < continueCases.Count; index++ ) {
                    var continueCase = continueCases[index];
                    var toNop = methodDefinition.Body.Instructions[methodDefinition.Body.Instructions.IndexOf( continueCase )+1];
                    var toJumpTo = targets.Single( x =>x.Operand!=null&& ( (string) x.Operand ).EndsWith( ( (string) continueCase.Operand ).Replace( "JUMPTOHACK", string.Empty ) ) );
                    var indexOf = methodDefinition.Body.Instructions.IndexOf( toJumpTo );

                    var toNop2 = methodDefinition.Body.Instructions[indexOf+1];

                    var nop1 = ilProcessor.Create( OpCodes.Nop );
                    toNop.OpCode = nop1.OpCode;
                    toNop.Operand = nop1.Operand;
                    var nop2 = ilProcessor.Create( OpCodes.Nop );
                    toNop2.OpCode = nop2.OpCode;
                    toNop2.Operand = nop2.Operand;
                    var nop3 = ilProcessor.Create(OpCodes.Nop);
                    toJumpTo.OpCode = nop3.OpCode;
                    toJumpTo.Operand = nop3.Operand;
                    
                    var instruction = methodDefinition.Body.Instructions[indexOf];

                    var instruction1 = ilProcessor.Create( OpCodes.Br, instruction );
                    continueCase.OpCode = instruction1.OpCode;
                    continueCase.Operand = instruction1.Operand;
                }

                var unusedTargets = methodDefinition.Body.Instructions.Where(x => x.OpCode == OpCodes.Ldstr).Where(x => ((string)x.Operand).StartsWith("JUMPABHACK")).ToList();
                for ( int index = 0; index < unusedTargets.Count; index++ ) {
                    var unusedTarget = unusedTargets[index];
                    var toNop = methodDefinition.Body.Instructions[methodDefinition.Body.Instructions.IndexOf( unusedTarget )+1];

                    var nop4 = ilProcessor.Create( OpCodes.Nop );
                    unusedTarget.OpCode = nop4.OpCode;
                    unusedTarget.Operand = nop4.Operand;
                    var nop5 = ilProcessor.Create( OpCodes.Nop );
                    toNop.OpCode = nop5.OpCode;
                    toNop.Operand = nop5.Operand;
                    }

                methodDefinition.Body.OptimizeMacros();
            }

            var toReturn = new MemoryStream();
            assemblyDefinition.Write( toReturn );
            return toReturn.ToArray();
        }
    }
}