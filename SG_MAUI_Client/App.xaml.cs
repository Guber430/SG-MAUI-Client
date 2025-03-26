using SG_MAUI_Client.Data;
using SG_MAUI_Client.Models;

namespace SG_MAUI_Client
{
    public partial class App : Application
    {
        internal ISportRepository sportRepository;
        internal IContingentRepository contingentRepository;
        internal IAthleteRepository athleteRepository;
        internal List<Athlete> Athletes;
        internal List<Sport> Sports;
        internal List<Contingent> Contingents;
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }
    }
}
