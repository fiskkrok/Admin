﻿
namespace SchoolApp.Admin.Domain.AggregateModels.StudentAggregate;

public class Student : BaseEntity, IAggregateRoot
{
    
    public string StudentId { get; set; } 
    [Required]
    public Address Address { get; set; } // Ensure there's a setter

    public string? FirstName { get; set; }
    public string? PhoneNumber { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? LastName { get; set; }

    public DateOnly? DateOfBirth { get; set; }
    public StudentStatus StudentStatus { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string? Email { get; set; }

    public DateOnly? EnrollmentDate { get; set; }

    private readonly List<Enrollment> _enrollments;
    private readonly List<Course> _courses;

    public IReadOnlyCollection<Enrollment> Enrollments => _enrollments.AsReadOnly();
    public IReadOnlyCollection<Course> Courses => _courses.AsReadOnly();

    // This constructor is for initial creation and other domain-specific operations
    public Student(string firstName, string lastName, DateOnly dateOfBirth, string email, Address address, Enrollment enrollment, string phoneNumber)
    {
        StudentId = Guid.NewGuid().ToString(); // Auto-generate StudentId
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        PhoneNumber = phoneNumber;
        Address = address;
        _enrollments = [enrollment];
        _enrollments.Remove(enrollment);
        _courses = new();
        // Ensure the student is active by default
        StudentStatus = StudentStatus.Active;
        // Ensure the enrollment date is set to the current date by default
        EnrollmentDate = DateOnly.FromDateTime(DateTime.Now);

    }

    // Parameterless constructor for EF and deserialization
    public Student()
    {

        Address = new Address("123 Main St", "Anytown", "WA", "USA", "12345");

    }


    public void UpdateStudent(string studentId, string firstName, string lastName, DateOnly? dateOfBirth, string email, Address address, string phoneNumber)
    {
        StudentId = studentId;
        FirstName = firstName;
        LastName = lastName;
        DateOfBirth = dateOfBirth;
        Email = email;
        Address = address;
        PhoneNumber = phoneNumber;
    }

    public void SetActiveStatus()
    {
        // Implementation here
    }
    

}
