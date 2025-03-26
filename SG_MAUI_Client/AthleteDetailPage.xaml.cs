using Microsoft.Maui.Graphics;
using SG_MAUI_Client.Models;
using SG_MAUI_Client.Utilities;
using System.Numerics;
using System.Text;

namespace SG_MAUI_Client;

public partial class AthleteDetailPage : ContentPage
{
    private Athlete athlete;
    private App thisApp;
    internal List<Sport> sports;
    internal List<Contingent> contingents;

    public AthleteDetailPage()
    {
        InitializeComponent();
        thisApp = Application.Current as App;
        sports = new List<Sport>();
        contingents = new List<Contingent>();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        athlete = (Athlete)this.BindingContext;

        // Set title and disable delete button if adding a new athlete
        if (athlete.ID == 0) // Adding New
        {
            this.Title = "Add New Athlete";
            btnDelete.IsEnabled = false;
        }
        else
        {
            this.Title = "Edit Athlete Details";
            btnDelete.IsEnabled = true;
        }

        FillSport();
        FillContingent();

        // Set the current value of Affiliation to the Entry field
        txtAffilitation.Text = athlete.Affiliation;
    }

    private void FillSport()
    {
        foreach (Sport s in thisApp.Sports.OrderBy(d => d.Name))
        {
            sports.Add(s);
        }
        // Fill the Drop Down Items for Sport
        ddlSports.ItemsSource = sports;

        // Set value to current athlete's sport
        if (athlete.SportID >= 0)
        {
            ddlSports.SelectedItem = thisApp.Sports.FirstOrDefault(d => d.ID == athlete.SportID);
        }
    }

    private void FillContingent()
    {
        foreach (Contingent c in thisApp.Contingents.OrderBy(d => d.Name))
        {
            contingents.Add(c);
        }
        // Fill the Drop Down Items for Contingent
        ddlContingents.ItemsSource = contingents;

        // Set value to current athlete's contingent
        if (athlete.ContingentID >= 0)
        {
            ddlContingents.SelectedItem = thisApp.Contingents.FirstOrDefault(d => d.ID == athlete.ContingentID);
        }
    }

    private async void SaveClicked(object sender, EventArgs e)
    {
        try
        {
            // Set selected sport
            athlete.Sport = (Sport)ddlSports.SelectedItem;
            athlete.SportID = athlete.Sport?.ID ?? 0;

            // Set selected contingent
            athlete.Contingent = (Contingent)ddlContingents.SelectedItem;
            athlete.ContingentID = athlete.Contingent?.ID ?? 0;

            // Set Affiliation from the Text input
            athlete.Affiliation = txtAffilitation.Text;

            // Validate selections
            if (athlete.SportID > 0 && athlete.ContingentID > 0)
            {
                if (athlete.ID == 0) // New athlete
                {
                    await thisApp.athleteRepository.AddAthlete(athlete);
                }
                else
                {
                    await thisApp.athleteRepository.UpdateAthlete(athlete);
                }
                await Navigation.PopAsync();
            }
            else
            {
                await DisplayAlert("Missing Information", "You must select both a sport and a contingent for the athlete.", "Ok");
            }
        }
        catch (ApiException apiEx)
        {
            var sb = new StringBuilder();
            sb.AppendLine("Errors:");
            foreach (var error in apiEx.Errors)
            {
                sb.AppendLine("-" + error);
            }
            await DisplayAlert("Problem Saving Athlete:", sb.ToString(), "Ok");
        }
        catch (Exception ex)
        {
            if (ex.GetBaseException().Message.Contains("connection with the server"))
            {
                await DisplayAlert("Error", "No connection with the server.", "Ok");
            }
            else
            {
                await DisplayAlert("Error", ex.GetBaseException().Message, "Ok");
            }
        }
    }

    private async void DeleteClicked(object sender, EventArgs e)
    {
        var answer = await DisplayAlert("Confirm Delete", "Are you certain that you want to delete " + athlete.Summary + "?", "Yes", "No");
        if (answer == true)
        {
            try
            {
                await thisApp.athleteRepository.DeleteAthlete(athlete);
                await Navigation.PopAsync();
            }
            catch (ApiException apiEx)
            {
                var sb = new StringBuilder();
                sb.AppendLine("Errors:");
                foreach (var error in apiEx.Errors)
                {
                    sb.AppendLine("-" + error);
                }
                await DisplayAlert("Problem Deleting Athlete:", sb.ToString(), "Ok");
            }
            catch (Exception ex)
            {
                if (ex.GetBaseException().Message.Contains("connection with the server"))
                {
                    await DisplayAlert("Error", "No connection with the server.", "Ok");
                }
                else
                {
                    await DisplayAlert("Error", ex.GetBaseException().Message, "Ok");
                }
            }
        }
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Navigation.PopAsync();
    }
}
