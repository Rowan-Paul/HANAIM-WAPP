namespace Inside_Airbnb.Shared
{
    public partial class Listing
    {
        public long? Id { get; set; }
        public string? ListingUrl { get; set; }
        public double? ScrapeId { get; set; }
        public DateTime? LastScraped { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? NeighborhoodOverview { get; set; }
        public string? PictureUrl { get; set; }
        public long? HostId { get; set; }
        public string? HostUrl { get; set; }
        public string? HostName { get; set; }
        public DateTime? HostSince { get; set; }
        public string? HostLocation { get; set; }
        public string? HostAbout { get; set; }
        public string? HostResponseTime { get; set; }
        public string? HostResponseRate { get; set; }
        public string? HostAcceptanceRate { get; set; }
        public string? HostIsSuperhost { get; set; }
        public string? HostThumbnailUrl { get; set; }
        public string? HostPictureUrl { get; set; }
        public string? HostNeighbourhood { get; set; }
        public int? HostListingsCount { get; set; }
        public int? HostTotalListingsCount { get; set; }
        public string? HostVerifications { get; set; }
        public string? HostHasProfilePic { get; set; }
        public string? HostIdentityVerified { get; set; }
        public string? Neighbourhood { get; set; }
        public string? NeighbourhoodCleansed { get; set; }
        public string? NeighbourhoodGroupCleansed { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public string? PropertyType { get; set; }
        public string? RoomType { get; set; }
        public string? Accommodates { get; set; }
        public int? Bathrooms { get; set; }
        public string? BathroomsText { get; set; }
        public int? Bedrooms { get; set; }
        public int? Beds { get; set; }
        public string? Amenities { get; set; }
        public int? Price { get; set; }
        public int? MinimumNights { get; set; }
        public int? MaximumNights { get; set; }
        public int? MinimumMinimumNights { get; set; }
        public int? MaximumMinimumNights { get; set; }
        public int? MinimumMaximumNights { get; set; }
        public int? MaximumMaximumNights { get; set; }
        public double? MinimumNightsAvgNtm { get; set; }
        public double? MaximumNightsAvgNtm { get; set; }
        public DateTime? CalendarUpdated { get; set; }
        public string? HasAvailability { get; set; }
        public int? Availability30 { get; set; }
        public int? Availability60 { get; set; }
        public int? Availability90 { get; set; }
        public int? Availability365 { get; set; }
        public DateTime? CalendarLastScraped { get; set; }
        public int? NumberOfReviews { get; set; }
        public int? NumberOfReviewsLtm { get; set; }
        public int? NumberOfReviewsL30d { get; set; }
        public DateTime? FirstReview { get; set; }
        public DateTime? LastReview { get; set; }
        public double? ReviewScoresRating { get; set; }
        public double? ReviewScoresAccuracy { get; set; }
        public double? ReviewScoresCleanliness { get; set; }
        public double? ReviewScoresCheckin { get; set; }
        public double? ReviewScoresCommunication { get; set; }
        public double? ReviewScoresLocation { get; set; }
        public double? ReviewScoresValue { get; set; }
        public string? License { get; set; }
        public string? InstantBookable { get; set; }
        public int? CalculatedHostListingsCount { get; set; }
        public int? CalculatedHostListingsCountEntireHomes { get; set; }
        public int? CalculatedHostListingsCountPrivateRooms { get; set; }
        public int? CalculatedHostListingsCountSharedRooms { get; set; }
        public double? ReviewsPerMonth { get; set; }
    }
}
