using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IBørnService

    {
        IEnumerable<Børn> GetBørn();

        Børn GetBørn(int id);
        Børn CreateBørn(Børn børn);
        Børn UpdateBørn(Børn børn);
        Børn DeleteBørn(Børn børn);

        IEnumerable<Børn> GetAllBørnDescending();
    }
}
