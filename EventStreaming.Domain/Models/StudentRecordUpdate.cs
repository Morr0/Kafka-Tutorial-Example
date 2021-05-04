namespace EventStreaming.Domain.Models
{
    public class StudentRecordUpdate
    {
        public string StudentId { get; set; }
        public StudentRecordState State { get; set; }
    }
}