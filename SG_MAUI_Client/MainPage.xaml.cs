using Microsoft.Maui.Graphics;
using SG_MAUI_Client.Data;
using SG_MAUI_Client.Models;
using SG_MAUI_Client.Utilities;
using System.Numerics;
using System.Text;

namespace SG_MAUI_Client
{
    public partial class MainPage : ContentPage
    {
        private App thisApp;
        private List<Athlete> athletes;
        private bool needRefresh;

        public MainPage()
        {
            InitializeComponent();
            thisApp = Application.Current as App;
            thisApp.sportRepository = new SportRepository();
            thisApp.athleteRepository = new AthleteRepository();
            thisApp.contingentRepository = new ContingentRepository();
            thisApp.Sports = new List<Sport>();
            thisApp.Contingents = new List<Contingent>();
            needRefresh = true;
            athletes = new List<Athlete>();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await ShowData();
        }

        private async Task ShowData()
        {
            btnAdd.IsEnabled = false;

            if (needRefresh)
            {
                await ShowSports();
                await ShowAthletes(null, null);
                await ShowContingents();
                AthleteList.SelectedItem = null;
                needRefresh = false;
            }
            else
            {
                int? sportID = (ddlSports.SelectedIndex > 0) ? ((Sport)ddlSports.SelectedItem).ID : null;
                int? contingentID = (ddlContingents.SelectedIndex > 0) ? ((Contingent)ddlContingents.SelectedItem).ID : null;
                await ShowAthletes(sportID, contingentID);
            }

            btnAdd.IsEnabled = true;
        }

        private async Task ShowSports()
        {
            if (thisApp.Sports?.Count == 0)
            {
                Loading.IsRunning = true;
                try
                {
                    thisApp.Sports = await thisApp.sportRepository.GetSports();
                }
                catch (Exception ex)
                {
                    await HandleException(ex, "Problem Getting List of Sports:");
                }
                finally
                {
                    Loading.IsRunning = false;
                }
            }
            List<Sport> sports = new() { new Sport { ID = 0, Name = "All Sports" } };
            sports.AddRange(thisApp.Sports.OrderBy(d => d.Name));
            ddlSports.ItemsSource = sports;
            ddlSports.SelectedIndex = 0;
        }

        private async Task ShowAthletes(int? sportID, int? contingentID)
        {
            Loading.IsRunning = true;
            try
            {
                athletes = await thisApp.athleteRepository.GetAthletes();

                if (sportID.HasValue && sportID > 0)
                {
                    athletes = athletes.Where(a => a.SportID == sportID.Value).ToList();
                }
                if (contingentID.HasValue && contingentID > 0)
                {
                    athletes = athletes.Where(a => a.ContingentID == contingentID.Value).ToList();
                }

                AthleteList.ItemsSource = athletes.OrderBy(p => p.LastName).ThenBy(p => p.FirstName);
                AthleteList.IsVisible = true;
            }
            catch (Exception ex)
            {
                await HandleException(ex, "Error Getting Athletes:");
                AthleteList.IsVisible = false;
            }
            finally
            {
                Loading.IsRunning = false;
            }
        }

        private async Task ShowContingents()
        {
            if (thisApp.Contingents?.Count == 0)
            {
                Loading.IsRunning = true;
                try
                {
                    thisApp.Contingents = await thisApp.contingentRepository.GetContingents();
                }
                catch (Exception ex)
                {
                    await HandleException(ex, "Problem Getting List of Contingents:");
                }
                finally
                {
                    Loading.IsRunning = false;
                }
            }
            List<Contingent> contingents = new() { new Contingent { ID = 0, Name = "All Contingents" } };
            contingents.AddRange(thisApp.Contingents.OrderBy(d => d.Name));
            ddlContingents.ItemsSource = contingents;
            ddlContingents.SelectedIndex = 0;
        }

        private async void btnAdd_Clicked(object sender, EventArgs e)
        {
            Athlete newAthlete = new() { DOB = DateTime.Today.AddDays(-1) };
            var athleteDetailPage = new AthleteDetailPage { BindingContext = newAthlete };
            AthleteList.IsVisible = false;
            await Navigation.PushAsync(athleteDetailPage);
        }

        private async void AthleteSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Athlete athlete)
            {
                var athleteDetailPage = new AthleteDetailPage { BindingContext = athlete };
                AthleteList.IsVisible = false;
                await Navigation.PushAsync(athleteDetailPage);
            }
        }

        private async void ddlSports_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? sportID = (ddlSports.SelectedIndex > 0) ? ((Sport)ddlSports.SelectedItem).ID : null;
            int? contingentID = (ddlContingents.SelectedIndex > 0) ? ((Contingent)ddlContingents.SelectedItem).ID : null;
            await ShowAthletes(sportID, contingentID);
        }

        private async void ddlContingents_SelectedIndexChanged(object sender, EventArgs e)
        {
            int? sportID = (ddlSports.SelectedIndex > 0) ? ((Sport)ddlSports.SelectedItem).ID : null;
            int? contingentID = (ddlContingents.SelectedIndex > 0) ? ((Contingent)ddlContingents.SelectedItem).ID : null;
            await ShowAthletes(sportID, contingentID);
        }

        private void btnRefresh_Clicked(object sender, EventArgs e)
        {
            needRefresh = true;
            _ = ShowData();
        }

        private async Task HandleException(Exception ex, string title)
        {
            var sb = new StringBuilder("Errors:\n");
            if (ex is ApiException apiEx)
            {
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("- " + error);
                }
            }
            else
            {
                sb.AppendLine(ex.GetBaseException().Message);
            }
            await DisplayAlert(title, sb.ToString(), "Ok");
        }
    }

}
