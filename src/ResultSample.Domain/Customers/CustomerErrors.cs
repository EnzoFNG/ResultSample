using ResultSample.Abstractions.Models;

namespace ResultSample.Domain.Customer;

public static class CustomerErrors
{
    public static Error CustomerAlreadyExistsError = new("Customer", "The customer with this email already exists.");
    public static Error CustomerNotExistsError = new("Customer", "The customer with this Id not exists.");

    public static Error EmailIsInvalidError = new("Customer.Email", "The customer email is invalid.");
    public static Error EmailLengthError = new("Customer.Email", "The customer email address should be a maximum of 150 characters.");

    public static Error NameIsEmptyError = new("Customer.Name", "The customer name is empty.");
    public static Error NameLengthError = new("Customer.Name", "The customer name address should be a minimum of 2 and a maximum of 100 characters.");

    public static Error AgeLowerThan18Error = new("Customer.Age", "The customer age is lower than 18.");
}