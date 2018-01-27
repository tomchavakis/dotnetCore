using System.ComponentModel.DataAnnotations;

namespace WebApp.Model
{
    public class Message
    {
        /// <summary>
        /// 
        /// </summary>
        [Key]
        public int Id { get; set; }
        public int MessageId { get; set; }
        public string Payload { get; set; }
    }
}