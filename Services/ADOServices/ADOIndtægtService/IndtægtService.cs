using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.ADOBørnService;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOIndtægtService
{
    public class IndtægtService : IIndtægtService
    {
        private AdonetIndtægtService _indtægtService;
        public IndtægtService(AdonetIndtægtService indtægtService)
        {
            _indtægtService = indtægtService;
        }
        public IEnumerable<Indtægt> GetIndtægter()
        {
            return _indtægtService.GetAllIndtægter();
        }

        public Indtægt GetIndtægtById(int id)
        {
            return _indtægtService.GetIndtægtById(id);
        }

        public Indtægt CreateIndtægt(Indtægt indtægt)
        {
            return _indtægtService.CreateIndtægt(indtægt);
        }

        public Indtægt DeleteIndtægt(Indtægt indtægt)
        {
            return _indtægtService.DeleteIndtægt(indtægt);
        }

        public Indtægt UpdateIndtægt(Indtægt indtægt)
        {
            return _indtægtService.UpdateIndtægt(indtægt);
        }


        public IEnumerable<Indtægt> GetIndtægtIDDESC()
        {
            return _indtægtService.GetIndtægtIDDESC();
        }

        public IEnumerable<Indtægt> GetIndtægtIDASC()
        {
            return _indtægtService.GetIndtægtIDASC();
        }


        public IEnumerable<Indtægt> GetAntalSolgteLodseddelerDESC()
        {
            return _indtægtService.GetAntalSolgteLodseddelerDESC();
        }

        public IEnumerable<Indtægt> GetAntalSolgteLodseddelerASC()
        {
            return _indtægtService.GetAntalSolgteLodseddelerASC();
        }

        public IEnumerable<Indtægt> GetAntalSolgteLodseddelerForGruppenASC()
        {
            return _indtægtService.GetAntalSolgteLodseddelerForGruppenASC();
        }

        public IEnumerable<Indtægt> GetAntalSolgteLodseddelerForGruppenDESC()
        {
            return _indtægtService.GetAntalSolgteLodseddelerForGruppenDESC();
        }

    }
}
