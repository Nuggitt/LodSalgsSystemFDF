using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IIndtaegtService
    {
        IEnumerable<Indtaegt> GetIndtaegter();

        Indtaegt GetIndtaegtById(int id);

        Indtaegt CreateIndtaegt(Indtaegt indtaegt);

        Indtaegt DeleteIndtaegt(Indtaegt indtaegt);

        Indtaegt UpdateIndtaegt(Indtaegt indtaegt);

        IEnumerable<Indtaegt> GetIndtaegtIDDESC();

        IEnumerable<Indtaegt> GetIndtaegtIDASC();

        IEnumerable<Indtaegt> GetAntalSolgteLodseddelerDESC();

        IEnumerable<Indtaegt> GetAntalSolgteLodseddelerASC();

        IEnumerable<Indtaegt> GetAntalSolgteLodseddelerForGruppenASC();

        IEnumerable<Indtaegt> GetAntalSolgteLodseddelerForGruppenDESC();


    }
}
