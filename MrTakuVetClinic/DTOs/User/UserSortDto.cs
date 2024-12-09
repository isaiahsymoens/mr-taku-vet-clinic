namespace MrTakuVetClinic.DTOs.User
{
    public class UserSortDto
    {
        public string SortBy { get; set; } = "Name";
        public bool Ascending { get; set; } = true;
    }
}
