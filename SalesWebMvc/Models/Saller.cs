using System.ComponentModel.DataAnnotations;

namespace SalesWebMvc.Models
{
    public class Saller
    {

        public int Id { get; set; }


        [Required(ErrorMessage ="{0} required")]

        //definindo regra de campo os numeros definem os valores colocados nos parametros
        [StringLength(60,MinimumLength =3, ErrorMessage ="{0} size should be between {2} and {1}")]
        public string Name { get; set; }


        [Required(ErrorMessage = "{0} required")]
        [EmailAddress(ErrorMessage = "Enter a valid email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }


        //definindo o nome que vai no front enves de vir apenas o nome do atributo
        [Required(ErrorMessage = "{0} required")]
        [Display (Name= "Birth Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime BirthDate { get; set; }

        [Required(ErrorMessage = "{0} required")]
        [Range(100.0, 50000.0, ErrorMessage = "{0} must be from {1} to {2}")]
        [Display(Name = "Base Salary")]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double BaseSalary { get; set; }

        //Associações com base no diagrama
        public Department Department { get; set; }

        [Required(ErrorMessage = "Department is required")]
        public int? DepartmentId { get; set; }


        public ICollection<SalesRecord> Sales { get; set; } = new List<SalesRecord>();

        public Saller(){}


        //nunca colocar no construtor atributos e forem coleções
        public Saller(int id, string name, string email, DateTime birthDate, double baseSalary, Department department)
        {
            Id = id;
            Name = name;
            Email = email;
            BirthDate = birthDate;
            BaseSalary = baseSalary;
            Department = department;
        }

        public void AddSales(SalesRecord sales)
        {
            Sales.Add(sales);
        }        
        
        public void RemoveSales(SalesRecord sales)
        {
            Sales.Remove(sales);
        }

        public double TotalSales(DateTime initial, DateTime final)
        {
            return Sales.Where(s => s.Date >= initial && s.Date <= final).Sum(s => s.Amount);
        }

    }
}
