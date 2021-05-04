using System;
using System.Text.Json;
using System.Threading.Tasks;
using Confluent.Kafka;
using EventStreaming.Domain.Models;
using EventStreaming.PublicApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace EventStreaming.PublicApi.Controllers
{
    [ApiController]
    [Route("Record")]
    public class StudentRecordController : ControllerBase
    {
        private readonly IProducer<Null, string> _producer;

        public StudentRecordController(IProducer<Null, string> producer)
        {
            _producer = producer;
        }
        
        [HttpPut("Update")]
        public async Task<IActionResult> UpdateStudentRecord([FromBody] StudentRecordUpdateRequest request)
        {
            var studentRecordUpdate = new StudentRecordUpdate
            {
                State = Enum.Parse<StudentRecordState>(request.State),
                StudentId = request.StudentId
            };
            string serializedObj = JsonSerializer.Serialize(studentRecordUpdate);
            
            await _producer.ProduceAsync("Student", new Message<Null, string>
            {
                Value = serializedObj,
                Timestamp = new Timestamp(DateTime.UtcNow)
            });

            return Ok();
        }
    }
}