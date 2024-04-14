namespace SchoolApp.Admin.Web.Models;

public class Student
{

    public string StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string? Email { get; set; }
    public Address Address { get; set; }
    public string PhoneNumber { get; set; }
    // Include Address and any other relevant properties
}

public class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }
    public string? Country { get; set; }
}

