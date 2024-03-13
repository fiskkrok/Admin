using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

namespace Admin.Application.AggregateModels.StudentAggregate;

public class Student : BaseEntity, IAggregateRoot
{
    //[Key]
    //[Column("StudentID")]
    public int StudentId { get; set; }
    [Required]
    public Address Address { get; private set; }

    public string? FirstName { get; set; }

    //[StringLength(50)]
    //[Unicode(false)]
    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }
    public StudentStatus StudentStatus { get; set; }
    //[StringLength(100)]
    //[Unicode(false)]
    public string? Email { get; set; }



    public DateOnly? EnrollmentDate { get; set; }
    // DDD Patterns comment
    // Using a private collection field, better for DDD Aggregate's encapsulation
    // so OrderItems cannot be added from "outside the AggregateRoot" directly to the collection,
    // but only through the method OrderAggregateRoot.AddOrderItem() which includes behavior.
    private readonly List<Enrollment> _enrollment;
    //[InverseProperty("Student")]
    public IReadOnlyCollection<Enrollment> Enrollments => _enrollment.AsReadOnly();
    protected Student()
    {
        _enrollment = new List<Enrollment>();
    }

    public Student(int studentId, string firstName, string lastName, DateOnly dateOfBirth, string email,
        Address address)
    {
        StudentId = studentId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        Address = address;
        _enrollment = new List<Enrollment>();
    }

    public void AddEnrollment(Enrollment enrollment)
    {
        _enrollment.Add(enrollment);
    }

    public void RemoveEnrollment(Enrollment enrollment)
    {
        _enrollment.Remove(enrollment);
    }

    public void UpdateStudent(int studentId, string firstName, string lastName, DateOnly dateOfBirth, string email,
        Address address)
    {
        StudentId = studentId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        Address = address;
    }

    public void SetActiveStatus()
    {
        
    }
}
