using Newtonsoft.Json;
namespace Global;
public class EntityNotFoundException : Exception{}

public class EntityNotFoundException<T>: EntityNotFoundException{
    
    private object obj;
    public EntityNotFoundException(){

    }

    public EntityNotFoundException(object obj){
        this.obj = obj;
    }
    public override string Message => $@"Сущность {typeof(T).Name}{(obj != null?$" c {JsonConvert.SerializeObject(obj)}":"")} не найдена.";

    
}