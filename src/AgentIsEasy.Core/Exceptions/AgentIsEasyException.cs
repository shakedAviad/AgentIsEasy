namespace AgentIsEasy.Core.Exceptions;

public class AgentIsEasyException(string message, Exception? innerException = null) : Exception(message, innerException);
