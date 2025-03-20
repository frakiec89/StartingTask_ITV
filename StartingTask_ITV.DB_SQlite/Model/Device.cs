using System.ComponentModel.DataAnnotations;

namespace StartingTask_ITV.DB_SQlite.Model
{
    internal class Device
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; }

        internal  Core.Model.Device GetCoreDevice()
        {
            return new Core.Model.Device
            {
                Id = Id,
                Type = Type
            };
        }
    }
}
