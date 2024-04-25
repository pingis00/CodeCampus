namespace CodeCampus.Web.Models.Components;

public class StarRatingComponent
{
    public ImageComponent StarIcon { get; set; } = new ImageComponent();
    public ImageComponent EmptyStarIcon { get; set; } = new ImageComponent();
    public int NumberOfStars { get; set; }
    public int TotalStars { get; set; }
}
