namespace AutoJIT.CSharpConverter.ConversionModule
{
    public interface IContinueCaseMsilFixingService {
        byte[] Fix( byte[] assembly, string scriptClassName );
    }
}