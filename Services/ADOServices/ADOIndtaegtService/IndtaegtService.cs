using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBornService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOIndtaegtService
{
    public class IndtaegtService : IIndtaegtService
    {
        private AdonetIndtaegtService _indtaegtService;
        public IndtaegtService(AdonetIndtaegtService indtaegtService)
        {
            _indtaegtService = indtaegtService;
        }
        public IEnumerable<Indtaegt> GetIndtaegter()
        {
            return _indtaegtService.GetAllIndtaegter();
        }

        public Indtaegt GetIndtaegtById(int id)
        {
            return _indtaegtService.GetIndtaegtById(id);
        }

        public Indtaegt CreateIndtaegt(Indtaegt indtaegt)
        {
            return _indtaegtService.CreateIndtaegt(indtaegt);
        }

        public Indtaegt DeleteIndtaegt(Indtaegt indtaegt)
        {
            return _indtaegtService.DeleteIndtaegt(indtaegt);
        }

        public Indtaegt UpdateIndtaegt(Indtaegt indtaegt)
        {
            return _indtaegtService.UpdateIndtaegt(indtaegt);
        }


        public IEnumerable<Indtaegt> GetIndtaegtIDDESC()
        {
            return _indtaegtService.GetIndtaegtIDDESC();
        }

        public IEnumerable<Indtaegt> GetIndtaegtIDASC()
        {
            return _indtaegtService.GetIndtaegtIDASC();
        }


        public IEnumerable<Indtaegt> GetAntalSolgteLodseddelerDESC()
        {
            return _indtaegtService.GetAntalSolgteLodseddelerDESC();
        }

        public IEnumerable<Indtaegt> GetAntalSolgteLodseddelerASC()
        {
            return _indtaegtService.GetAntalSolgteLodseddelerASC();
        }

        public IEnumerable<Indtaegt> GetAntalSolgteLodseddelerForGruppenASC()
        {
            return _indtaegtService.GetAntalSolgteLodseddelerForGruppenASC();
        }

        public IEnumerable<Indtaegt> GetAntalSolgteLodseddelerForGruppenDESC()
        {
            return _indtaegtService.GetAntalSolgteLodseddelerForGruppenDESC();
        }

    }
}
