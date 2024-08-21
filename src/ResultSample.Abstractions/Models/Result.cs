namespace ResultSample.Abstractions.Models;

public class Result
{
    protected readonly List<Error> _errors = [];

    public static Result Empty() => new();
    public static Result Success(object response) => new(response);
    public static Result Failure(List<Error> errors) => new(errors);

    protected Result()
    {
        IsSuccess = true;
        Response = null;
    }

    protected Result(object response)
    {
        IsSuccess = true;
        Response = response;
    }

    protected Result(List<Error> errors)
    {
        ArgumentNullException.ThrowIfNull(errors);

        var errorThatCodeIsWrong = errors.FirstOrDefault(x => x.Key.Equals(Error.None.Key) || string.IsNullOrWhiteSpace(x.Key));

        if (errorThatCodeIsWrong is not null)
        {
            throw new ArgumentException("Invalid error", nameof(errorThatCodeIsWrong));
        }

        IsSuccess = false;
        Response = null;
        _errors.AddRange(errors);
    }

    public virtual object? Response { get; }
    public virtual bool IsSuccess { get; }
    public virtual bool IsFailure => !IsSuccess;
    public virtual IReadOnlyCollection<Error>? Errors => _errors.AsReadOnly();

    public virtual Result Match<TObject>(Func<TObject, Result> onSuccess, Func<List<Error>, Result> onFail)
    {
        return IsSuccess
            ? onSuccess((TObject)Response!)
            : onFail([.. Errors]!);
    }

    public virtual async Task<Result> Match<TObject>(
        Func<TObject, Task<Result>> onSuccessAsync, 
        Func<List<Error>, Task<Result>> onFailAsync)
    {
        return IsSuccess
            ? await onSuccessAsync((TObject)Response!)
            : await onFailAsync([.. Errors]!);
    }

    public virtual async Task<Result> Match<TObject>(
        Func<TObject, Task<Result>> onSuccessAsync,
        Func<List<Error>, Result> onFail)
    {
        return IsSuccess
            ? await onSuccessAsync((TObject)Response!)
            : onFail([.. Errors]!);
    }

    public virtual async Task<Result> Match<TObject>(
        Func<TObject, Result> onSuccess,
        Func<List<Error>, Task<Result>> onFailAsync)
    {
        return IsSuccess
            ? onSuccess((TObject)Response!)
            : await onFailAsync([.. Errors]!);
    }

    public virtual Result IfFail(Action<List<Error>> func)
    {
        if (IsFailure)
        {
            func([.. Errors!]!);
        }

        return this;
    }

    public virtual Result IfSuccess(Action<object> func)
    {
        if (IsSuccess)
            func(Response!);

        return this;
    }

    public static implicit operator Result(Error error)
        => new([error]);

    public static implicit operator Result(List<Error> errors)
        => new(errors);
}