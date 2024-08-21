using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ResultSample.Abstractions.Models;

public abstract class ErrorObject
{
    protected readonly List<Error> _errors = [];

    protected ErrorObject()
    { }

    protected internal abstract void Validate();

    [NotMapped]
    [BindNever]
    [JsonIgnore]
    public bool IsValid => _errors.Count == 0;
    [NotMapped]
    [BindNever]
    [JsonIgnore]
    public IReadOnlyCollection<Error> Errors => _errors.AsReadOnly();

    #region Error Management
    public virtual void AddError(Error error)
    {
        _errors.Add(error);
    }

    public virtual void AddErrors(List<Error> errors)
    {
        _errors.AddRange(errors);
    }

    public virtual void AddErrorWhen(bool condition, Error error)
    {
        if (!condition)
            return;

        _errors.Add(error);
    }

    public virtual void AddErrorsWhen(bool condition, List<Error> errors)
    {
        if (!condition)
            return;

        _errors.AddRange(errors);
    }

    public virtual void RemoveError(Error error)
    {
        _errors.Remove(error);
    }

    public virtual void RemoveErrors(List<Error> errors)
    {
        foreach (var error in errors)
            _errors.Remove(error);
    }

    public virtual void RemoveErrorWhen(bool condition, Error error)
    {
        if (!condition)
            return;

        _errors.Remove(error);
    }

    public virtual void RemoveErrorsWhen(bool condition, List<Error> errors)
    {
        if (!condition)
            return;

        foreach (var error in errors)
        {
            _errors.Remove(error);
        }
    }

    public void ClearErrors(Error error)
    {
        _errors.Clear();
    }
    #endregion
}