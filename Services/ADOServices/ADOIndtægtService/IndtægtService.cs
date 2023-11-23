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
    }
}
