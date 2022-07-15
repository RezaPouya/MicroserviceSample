namespace UserService;

public static class UserServiceErrorCodes
{
    //Add your business exception error codes here...
    public const string UserInputNotValid = "UserService:User:UserInputNotValid:0000";
    public const string UserIdNotValid = "UserService:User:UserIdNotValid:0001";
    public const string UserIdDuplicated = "UserService:User:UserIdDuplicated:0002";
    public const string UserNotFound = "UserService:User:UserNotFound:0003";
}
