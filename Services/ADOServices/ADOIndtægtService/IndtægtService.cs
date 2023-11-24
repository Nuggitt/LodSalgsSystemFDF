using LodSalgsSystemFDF.Models;
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

        
    }
}
