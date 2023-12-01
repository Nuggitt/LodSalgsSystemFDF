﻿using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IBørnService

    {
        IEnumerable<Børn> GetBørn();

        Børn GetBørn(int id);
        Børn CreateBørn(Børn børn);
        Børn UpdateBørn(Børn børn);
        Børn DeleteBørn(Børn børn);

        IEnumerable<Børn> GetAllBørnNavnDescending();
        IEnumerable<Børn> GetAllBørnIDDescending();
        IEnumerable<Børn> GetAllBørnAntalSolgteLodseddelerDescending();
        IEnumerable<Børn> GetAllBørnGruppeIDDescending();

        IEnumerable<Børn> GetAllBørnNavnAscending();
        IEnumerable<Børn> GetAllBørnIDAscending();
        IEnumerable<Børn> GetAllBørnAntalSolgteLodseddelerAscending();
        IEnumerable<Børn> GetAllBørnGruppeIDAscending();

    }
}
