using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IIndtægtService
    {
        IEnumerable<Indtægt> GetIndtægter();
    }
}
