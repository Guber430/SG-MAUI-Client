using SG_MAUI_Client.Utilities;
using SG_MAUI_Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SG_MAUI_Client.Data
{
    internal class ContingentRepository : IContingentRepository
    {
        private readonly HttpClient client = new HttpClient();

        public ContingentRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task AddContingent(Contingent contingentToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/contingent", contingentToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteContingent(Contingent contingentToDelete)
        {
            var response = await client.DeleteAsync($"/api/contingent/{contingentToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<Contingent> GetContingent(int ContingentID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/contingent/{ContingentID}");
            if (response.IsSuccessStatusCode)
            {
                Contingent contingent = await response.Content.ReadAsAsync<Contingent>();
                return contingent;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            };
        }

        public async Task<List<Contingent>> GetContingents()
        {
            HttpResponseMessage response = await client.GetAsync($"api/contingent");
            if (response.IsSuccessStatusCode)
            {
                List<Contingent> contingents = await response.Content.ReadAsAsync<List<Contingent>>();
                return contingents;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            };
        }

        public async Task<List<Contingent>> GetContingentsInc(int AthleteID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/contingent/byAthlete/{AthleteID}");
            if (response.IsSuccessStatusCode)
            {
                List<Contingent> Contingents = await response.Content.ReadAsAsync<List<Contingent>>();
                return Contingents;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateContingente(Contingent contingentToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/contingent/{contingentToUpdate.ID}", contingentToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
