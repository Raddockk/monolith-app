namespace Global;
public class BadLoginException : Exception{
    public override string Message => "Неверный логин/пароль";
}