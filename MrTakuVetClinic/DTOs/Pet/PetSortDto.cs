namespace MrTakuVetClinic.DTOs.Pet
{
    public class PetSortDto
    {
        public string SortBy { get; set; } = "PetName";
        public bool Ascending { get; set; } = true;
    }
}
