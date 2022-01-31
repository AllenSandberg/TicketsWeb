using System.Collections.Generic;

namespace PenaltiesManagement.ViewModel
{
    public class DashboardViewModel
    {
        public List<DashboardStatisticsMapViewModel> CountryMapStatistics { get; set; }
    }

    public class DashboardStatisticsMapViewModel
    {
        public string CountryName { get; set; }
        public int ClientsNumber { get; set; }
        public int ActiveClientsNumber { get; set; }
        public int PaymentsNumber { get; set; }
        public int ClientsRegisteredToday { get; set; }
        public double PercentageOfTotalClients { get; set; }
    }
}
