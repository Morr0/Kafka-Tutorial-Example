using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using EventStreaming.Domain.Models;

namespace EventStreaming.PublicApi.Dtos
{
    public class StudentRecordUpdateRequest : IValidatableObject
    {
        [Required] public string StudentId { get; set; }
        [Required] public string State { get; set; }
        
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var states = Enum.GetNames(typeof(StudentRecordState));
            bool parsable = Enum.TryParse<StudentRecordState>(State, out _);
            if (!parsable) yield return new ValidationResult(
                $"The state must be one of the following: {string.Join(',', states)}");
        }
    }
}