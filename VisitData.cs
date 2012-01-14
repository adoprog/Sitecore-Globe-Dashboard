namespace Sitecore.SharedSource.GlobeDashboard
{
    /// <summary>
    /// Visit Data
    /// </summary>
    public class VisitData
    {
        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        /// <value>The latitude.</value>
        public double Latitude { get; set; }

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        /// <value>The longitude.</value>
        public double Longitude { get; set; }

        /// <summary>
        /// Gets or sets the page views.
        /// </summary>
        /// <value>The page views.</value>
        public int Value { get; set; }

        /// <summary>
        /// Gets or sets the type of the traffic.
        /// </summary>
        /// <value>The type of the traffic.</value>
        public int TrafficType { get; set; }
    }
}