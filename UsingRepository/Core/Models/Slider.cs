using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web;
using System.Web.Mvc;

namespace UsingRepository.Core.Models
{
    public class Slider
    {
        public int Id { get; set; }
        [StringLength(200)]
        public string ImagePath { get; set; }
        [StringLength(250)]

        public string Header { get; set; }
        [StringLength(250)]
        [AllowHtml]
        public string Paragraph { get; set; }
        [NotMapped]
        public HttpPostedFileBase PostedFileBase { get; set; }
        public bool IsDeleted { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

    }
}