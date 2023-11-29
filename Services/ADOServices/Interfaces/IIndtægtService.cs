using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IIndtægtService
    {
        IEnumerable<Indtægt> GetIndtægter();

        Indtægt GetIndtægtById(int id);

        Indtægt CreateIndtægt(Indtægt indtægt);

        Indtægt DeleteIndtægt(Indtægt indtægt);

        Indtægt UpdateIndtægt(Indtægt indtægt);

        
    }
}
