namespace MetadataGenerator.Definitions;

public interface ICommand : ICommand<object>;
public interface ICommand<TResponse>;