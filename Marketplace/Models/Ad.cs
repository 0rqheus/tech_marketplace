using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.IO;

namespace Marketplace.Models
{
    public abstract class Ad : Component
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
        public string Location { get; set; }
        public string Category { get; set; }

        public byte[] Photo { get; set; }

        public string Brand { get; set; }

        public List<int> Subscribers { get; set; } = new List<int>();
        public bool IsFreezed { get; set; }

        public Ad()
        { }

        public Ad(CreateViewModel vm)
        {
            this.Title = vm.Title;
            this.Description = vm.Description;
            this.Price = vm.Price;
            this.CreationDate = DateTime.Now;
            this.Location = vm.Location;
            this.Category = vm.Category;
            using (var binaryReader = new BinaryReader(vm.PhotoFile.OpenReadStream()))
            {
                this.Photo = binaryReader.ReadBytes((int)vm.PhotoFile.Length);
            }
            this.Brand = vm.Brand;
        }

        public override string GetImgSrc()
        {
            return "";
        }

        public abstract Dictionary<string, string> GetAdditionalProperties();
    }
}
