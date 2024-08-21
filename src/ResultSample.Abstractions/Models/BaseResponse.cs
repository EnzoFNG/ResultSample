namespace ResultSample.Abstractions.Models;

public abstract record BaseResponse<TResponse, TSourceMap> where TResponse : BaseResponse<TResponse, TSourceMap>
{
    public abstract TResponse Map(TSourceMap sourceObject);
}