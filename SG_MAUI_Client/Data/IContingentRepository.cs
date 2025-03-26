using SG_MAUI_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_Client.Data
{
    internal interface IContingentRepository
    {
        Task<List<Contingent>> GetContingents();
        Task<Contingent> GetContingent(int ID);
        Task<List<Contingent>> GetContingentsInc(int AthleteID);
        Task AddContingent(Contingent contingentToAdd);
        Task UpdateContingente(Contingent contingentToUpdate);
        Task DeleteContingent(Contingent contingentToDelete);
    }
}
