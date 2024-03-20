﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.InteropServices;
using Admin.Application.AggregateModels.EnrollmentAggregate;
using Admin.Application.SeedWork;
using Admin.Domain.SeedWork;

using Microsoft.EntityFrameworkCore;

namespace Admin.Application.AggregateModels.StudentAggregate;

public class Student : BaseEntity, IAggregateRoot
{
    public int StudentId { get; set; }
    [Required]
    public Address Address { get; set; } // Ensure there's a setter

    public string? FirstName { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }
    public StudentStatus StudentStatus { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    private readonly List<Enrollment> _enrollment;

    public IReadOnlyCollection<Enrollment> Enrollments => _enrollment.AsReadOnly();

    // This constructor is for initial creation and other domain-specific operations
    public Student(int studentId, string firstName, string lastName, DateOnly dateOfBirth, string email, Address address)
    {
        StudentId = studentId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        Address = address;
        _enrollment = new List<Enrollment>();
        // Ensure the student is active by default
        StudentStatus = StudentStatus.Active;
        // Ensure the enrollment date is set to the current date by default
        EnrollmentDate = DateOnly.FromDateTime(DateTime.Now);

    }

    // Parameterless constructor for EF and deserialization
    public Student()
    {
        _enrollment = new List<Enrollment>();
        // Initialize Address with default non-null values
      Address = new Address("123 Main St", "Anytown", "WA", "USA", "12345");

    }


    public void AddEnrollment(Enrollment enrollment)
    {
        _enrollment.Add(enrollment);
    }

    public void RemoveEnrollment(Enrollment enrollment)
    {
        _enrollment.Remove(enrollment);
    }

    public void UpdateStudent(int studentId, string firstName, string lastName, DateOnly? dateOfBirth, string email, Address address)
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
        // Implementation here
    }
    

}