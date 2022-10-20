using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Soil.Models
{
    public class MoistureReading
    {
        public int? Id { get; set; }
        public string? Location { get; set; }

        [Display(Name = "Timestamp")]
        [DataType(DataType.DateTime)]
        public DateTime? Timestamp { get; set; }
      
        public int? RawReading { get; set; }

    }
}