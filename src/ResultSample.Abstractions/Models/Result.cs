namespace ResultSample.Abstractions.Models;

public class Result
{
    protected readonly List<Error> _errors = [];

    public static new Result Success(object response) => new(response);
    public static Result Failure(List<Error> errors) => new(errors);

    protected Result()
    {
        IsSuccess = true;
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
        _errors.AddRange(errors);
    }

    public virtual object? Response { get; }

    public virtual IReadOnlyCollection<Error>? Errors => _errors.AsReadOnly();

    public virtual bool IsSuccess { get; }

    public virtual bool IsFailure => !IsSuccess;

    public virtual R Match<R>(Func<R> onSuccess, Func<List<Error>, R> onFailed)
    {
        return IsSuccess
            ? onSuccess()
            : onFailed([.. Errors!]);
    }

    public virtual Result IfFail(Action<List<Error>> func)
    {
        if (IsFailure)
        {
            func([.. Errors!]!);
        }

        return this;
    }

    public virtual Result IfSuccess(Action func)
    {
        if (IsSuccess)
            func();

        return this;
    }

    public static implicit operator Result(Error error)
        => new([error]);

    public static implicit operator Result(List<Error> errors)
        => new(errors);
}

public class Result<T> : Result
{
   

    

    public virtual R Match<R>(Func<T, R> onSuccess, Func<List<Error>, R> onFail)
    {
        return IsSuccess
            ? onSuccess(Response!)
            : onFail([.. Errors!]);
    }

    public virtual T IfFail(Func<List<Error>, T> func)
    {
        return IsFailure
            ? func([.. Errors!])
            : Response!;
    }

    public virtual Result<T> IfSuccess(Action<T> func)
    {
        if (IsSuccess)
            func(Response!);

        return this;
    }

    public static implicit operator Result<T>(T value)
        => new(value);

    public static implicit operator Result<T>(Error error)
        => new([error]);

    public static implicit operator Result<T>(List<Error> errors)
       => new(errors);
}