using SG_MAUI_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_Client.Data
{
    internal interface ISportRepository
    {
        Task<List<Sport>> GetSports();
        Task<Sport> GetSport(int ID);
        Task<List<Sport>> GetSportsInc(int AthleteID);
        Task AddSport(Sport sportToAdd);
        Task UpdateSport(Sport sportToUpdate);
        Task DeleteSport(Sport sportToDelete);
    }
}
