namespace AccountingTools.Repository.Interface
{
    public interface IPdfRepository
    {
        List<string> RedPdf(MemoryStream pdf, string password);
    }
}
