namespace AccountingTools.Service.Interface
{
    public interface IBancolombiaService
    {
        Stream CreateConcilBancolombia(MemoryStream pdf, string password = null);
    }
}
