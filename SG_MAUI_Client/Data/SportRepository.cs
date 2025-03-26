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
    internal class SportRepository: ISportRepository
    {
        private readonly HttpClient client = new HttpClient();

        public SportRepository()
        {
            client.BaseAddress = Jeeves.DBUri;
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task AddSport(Sport sportToAdd)
        {
            var response = await client.PostAsJsonAsync("/api/sport", sportToAdd);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task DeleteSport(Sport sportToDelete)
        {
            var response = await client.DeleteAsync($"/api/sport/{sportToDelete.ID}");
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task<Sport> GetSport(int SportID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/sport/{SportID}");
            if (response.IsSuccessStatusCode)
            {
                Sport sport = await response.Content.ReadAsAsync<Sport>();
                return sport;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            };
        }

        public async Task<List<Sport>> GetSports()
        {
            HttpResponseMessage response = await client.GetAsync($"api/sport");
            if (response.IsSuccessStatusCode)
            {
                List<Sport> Sports = await response.Content.ReadAsAsync<List<Sport>>();
                return Sports;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            };
        }

        public async Task<List<Sport>> GetSportsInc(int AthleteID)
        {
            HttpResponseMessage response = await client.GetAsync($"api/sport/byAthlete/{AthleteID}");
            if (response.IsSuccessStatusCode)
            {
                List<Sport> Sports = await response.Content.ReadAsAsync<List<Sport>>();
                return Sports;
            }
            else
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }

        public async Task UpdateSport(Sport sportToUpdate)
        {
            var response = await client.PutAsJsonAsync($"/api/sport/{sportToUpdate.ID}", sportToUpdate);
            if (!response.IsSuccessStatusCode)
            {
                var ex = Jeeves.CreateApiException(response);
                throw ex;
            }
        }
    }
}
