namespace SalesWebMvc.Models.ViewModels
{
    public class SallerFormViewModel
    {
        public Saller Saller { get; set; }
        public ICollection<Department> Departments { get; set; }



    }
}
