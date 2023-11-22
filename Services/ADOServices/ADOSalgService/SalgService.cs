﻿using LodSalgsSystemFDF.Models;
using LodSalgsSystemFDF.Services.ADOServices.Interfaces;

namespace LodSalgsSystemFDF.Services.ADOServices.ADOSalgService
{
    public class SalgService : ISalgService
    {
        private AdonetSalgService _salgService;

        public SalgService(AdonetSalgService salgservice)
        {
            _salgService = salgservice;
        }

        public IEnumerable<Salg> GetSalgs()
        {
            return _salgService.GetAllSalgs();
        }

        public Salg GetSalgById(int id)
        {
            return _salgService.GetSalgById(id);
        }

        public Salg CreateSalg(Salg salg)
        {
            return _salgService.CreateSalg(salg);
        }

        public Salg DeleteSalg(Salg salg)
        {
            return _salgService.DeleteSalg(salg);
        }
    }
}
