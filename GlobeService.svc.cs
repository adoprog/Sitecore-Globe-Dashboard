namespace Sitecore.SharedSource.GlobeDashboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.ServiceModel.Activation;
    using System.ServiceModel.Web;

    /// <summary>
    /// Globe Dashboard Service
    /// </summary>
    [ServiceContract(Namespace = "")]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class GlobeService
    {
        /// <summary>
        /// Gets the visits.
        /// </summary>
        /// <param name="from">From date.</param>
        /// <param name="to">To date  .</param>
        /// <returns>Visits data</returns>
        [OperationContract]
        public double[] GetVisits(string from, string to)
        {
            var dateFrom = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(from)).ToLocalTime();
            var dateTo = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(double.Parse(to)).ToLocalTime();

            var visits = AnalyticsHelper.GetVisits(dateFrom, dateTo);
            var dataArray = new double[visits.Count * 4];
            int counter = 0;
            foreach (var eventData in visits)
            {
                dataArray[counter++] = eventData.Latitude;
                dataArray[counter++] = eventData.Longitude;
                dataArray[counter++] = eventData.Value / 100.0;
                dataArray[counter++] = GetColorIndexByTrafficType(eventData.TrafficType) * 1.0;
            }

            return dataArray;
        }

        /// <summary>
        /// Gets the traffic types.
        /// </summary>
        /// <returns>Traffic types</returns>
        [WebGet]
        [OperationContract]
        public string[] GetTrafficTypes()
        {
            var results = new List<string>();
            var trafficTypes = AnalyticsHelper.GetTrafficTypes();

            foreach (var type in trafficTypes)
            {
              results.Add(type.Text);
              results.Add(GetColorIndexByTrafficType(type.Type).ToString());
            }

            return results.ToArray();
        }

        /// <summary>
        /// Gets the type of the color index by traffic.
        /// </summary>
        /// <param name="trafficType">Type of the traffic.</param>
        /// <returns>Color by traffic type.</returns>
        private static int GetColorIndexByTrafficType(int trafficType)
        {
            var trafficTypes = AnalyticsHelper.GetTrafficTypes();

            for (int i = 0; i < trafficTypes.Count(); i++)
            {
                if (trafficTypes.ElementAt(i).Type == trafficType)
                {
                    return i;
                }
            }

            return 0;
        }
    }
}
