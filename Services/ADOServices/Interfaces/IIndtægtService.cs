﻿using LodSalgsSystemFDF.Models;

namespace LodSalgsSystemFDF.Services.ADOServices.Interfaces
{
    public interface IIndtægtService
    {
        IEnumerable<Indtægt> GetIndtægter();

        Indtægt GetIndtægtById(int id);

        Indtægt CreateIndtægt(Indtægt indtægt);

        Indtægt DeleteIndtægt(Indtægt indtægt);

        Indtægt UpdateIndtægt(Indtægt indtægt);

        IEnumerable<Indtægt> GetIndtægtIDDESC();

        IEnumerable<Indtægt> GetIndtægtIDASC();

        IEnumerable<Indtægt> GetAntalSolgteLodseddelerDESC();

        IEnumerable<Indtægt> GetAntalSolgteLodseddelerASC();

        IEnumerable<Indtægt> GetAntalSolgteLodseddelerForGruppenASC();

        IEnumerable<Indtægt> GetAntalSolgteLodseddelerForGruppenDESC();


    }
}
