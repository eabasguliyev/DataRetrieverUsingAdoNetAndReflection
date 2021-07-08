using PopulatingTable_Using_AdoNet_Reflection.CustomAttributes;

namespace PopulatingTable_Using_AdoNet_Reflection.Entities
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(Option = DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [MaxLength(Length = 255)]
        public string Vendor { get; set; }

        [Required]
        [MaxLength(Length = 255)]
        public string Model { get; set; }
        public float Engine { get; set; }
        
        
        public int Year { get; set; }

        public override string ToString()
        {
            return $"|{Vendor,20}|{Model,20}|{Engine,20}|{Year,20}|";
        }
    }
}