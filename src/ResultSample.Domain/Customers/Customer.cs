using ResultSample.Abstractions.Models;
using System.Net.Mail;

namespace ResultSample.Domain.Customer;

public sealed class Customer : BaseEntity
{
    private Customer()
    { }

    private Customer(Guid id, string email, string name, int age)
    {
        Id = id;
        Email = email;
        Name = name;
        Age = age;
    }

    public string Email { get; private set; } = string.Empty;
    public string Name { get; private set; } = string.Empty;
    public int Age { get; private set; }

    public static Result Create(string email, string name, int age)
    {
        var customer = new Customer(Guid.NewGuid(), email, name, age);

        if (!customer.IsValid)
            return customer.Errors.ToList();

        return Result.Success(customer);
    }

    public Result Edit(string? email = null, string? name = null, int? age = null)
    {
        Email = email ?? Email;
        Name = name ?? Name;
        Age = age ?? Age;

        if (!IsValid)
            return Errors.ToList();

        return Result.Success(this);
    }

    protected override void Validate()
    {
        AddErrorWhen(string.IsNullOrWhiteSpace(Email) || !IsValidEmail(Email), CustomerErrors.EmailIsInvalidError);
        AddErrorWhen(Email.Length > 150, CustomerErrors.EmailLengthError);

        AddErrorWhen(string.IsNullOrWhiteSpace(Name), CustomerErrors.NameIsEmptyError);
        AddErrorWhen(Name is { Length: < 2 or > 50 }, CustomerErrors.NameLengthError);

        AddErrorWhen(Age < 18, CustomerErrors.AgeLowerThan18Error);
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            _ = new MailAddress(email);
            return true;
        }
        catch
        {
            return false;
        }
    }
}