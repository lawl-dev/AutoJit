namespace AutoJIT.Compiler
{
    public interface IContinueCaseMsilFixingService {
        byte[] Fix( byte[] assembly, string scriptClassName );
    }
}